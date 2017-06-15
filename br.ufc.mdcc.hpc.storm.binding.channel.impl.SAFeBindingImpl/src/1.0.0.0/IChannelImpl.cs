
using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

using EnvelopType = System.Tuple<int,int,int,int,int,int>;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Net;

namespace br.ufc.mdcc.hpc.storm.binding.channel.impl.SAFeBindingImpl
{
	public class IChannelImpl : BaseIChannelImpl, IChannel
	{
		private IDictionary<int,Thread> thread_receive_requests = null;

		private static byte[] ObjectToByteArray(Object obj)
		{
			if(obj == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream ms = new MemoryStream();
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}

		private static Object ByteArrayToObject(byte[] arrBytes)
		{
			MemoryStream memStream = new MemoryStream();
			BinaryFormatter binForm = new BinaryFormatter();
			memStream.Write(arrBytes, 0, arrBytes.Length);
			memStream.Seek(0, SeekOrigin.Begin);
			Object obj = (Object) binForm.Deserialize(memStream);
			return obj;
		}

		#region implemented abstract members of BindingRoot
		public override void server ()
		{
			this.TraceFlag = true;

			Console.WriteLine(": BEFORE CREATE SOCKETS !!! " + this.ThisFacet + " / " + this.ThisFacetInstance + " : " + this.CID.getInstanceName());

			createSockets ();

			Console.WriteLine(": AFTER CREATED SOCKETS !!! " + this.ThisFacet + " / " + this.ThisFacetInstance + " : " + this.CID.getInstanceName());

			synchronizer_monitor = new SynchronizerMonitor (this, client_socket_facet, this.ThisFacetInstance, this.CID.getInstanceName());

			sockets_initialized_flag.Set ();

			// Create the threads that will listen the sockets for each other facet.
			thread_receive_requests = new Dictionary<int, Thread>();

			foreach (int facet in this.Facet.Keys) 
			{
				if (facet != this.ThisFacetInstance)
				{
					Console.WriteLine("loop create thread_receive_requests: " + facet + " / " + this.ThisFacetInstance);
					Socket server_socket = server_socket_facet [facet];
					thread_receive_requests[facet] = new Thread (new ThreadStart (() => synchronizer_monitor.serverReceiveRequests(facet, server_socket)));
					thread_receive_requests[facet].Start ();
				}
			}

			while (true) 
			{
				Thread.Sleep (100);
				synchronizer_monitor.serverReadRequest ();
			}

		}

		private AutoResetEvent sockets_initialized_flag = new AutoResetEvent(false);

		public override void client ()
		{
			this.TraceFlag = true;

			//while (!sockets_initialized_flag)	Thread.Sleep (100);
			sockets_initialized_flag.WaitOne ();

		}
		#endregion

		private SynchronizerMonitor synchronizer_monitor;

		private IDictionary<int, Socket> client_socket_facet = new Dictionary<int, Socket>();
		private IDictionary<int, Socket> server_socket_facet = new Dictionary<int, Socket>();

		private void connectSockets()
		{
			foreach (KeyValuePair<int, FacetAccess> facet_access in this.Facet) 
			{
				int facet = facet_access.Key;
				if (facet != this.ThisFacetInstance)
				{
					Socket socket = client_socket_facet [facet];
					IPEndPoint endPoint = end_point_client [facet];

					bool isConnected = false;
					int tries = 0;
					while (!isConnected && tries <=30) 
					{
						try {
							//Console.WriteLine("CONNECTING " + "facet=" + facet + " / " + endPoint+ " / " + this.CID.getInstanceName());
							socket.Connect (endPoint);
							isConnected = true;
							//Console.WriteLine("CONNECTED " + "facet=" + facet + " / " + endPoint+ " / " + this.CID.getInstanceName());
						}
						catch (Exception e) 
						{
							//tries ++;
							//isConnected = false;
							//Console.WriteLine("CONNECTION FAILED N --- ATTEMPT #" + tries + " *** " + e.Message + " --- " + endPoint);
							Thread.Sleep(1000);
						}
					}
					if (!isConnected) {
						Console.WriteLine("createSockets --- It was not possible to talk to the server");
						throw new Exception ("createSockets --- It was not possible to talk to the server");
					}
				}
			}
		}

		private void acceptSockets ()
		{
			foreach (KeyValuePair<int, FacetAccess> facet_access in this.Facet) 
			{
				int facet = facet_access.Key;
				if (facet != this.ThisFacetInstance) 
				{
					Socket socket = server_socket_facet [facet];
					IPEndPoint endPoint = end_point_server [facet];
					Console.WriteLine("BINDING " + endPoint + " -- " + facet + " / " + this.CID.getInstanceName());
					socket.Bind (endPoint) ;
					socket.Listen (10);
					server_socket_facet[facet] = socket.Accept ();
					Console.WriteLine("BINDED " + endPoint + " -- " + facet + " / " + this.CID.getInstanceName());
				}
			}
		}

		private IDictionary<int,IPEndPoint> end_point_client = new Dictionary<int,IPEndPoint>();
		private IDictionary<int,IPEndPoint> end_point_server = new Dictionary<int,IPEndPoint>();


		private void createSockets ()
		{
			FacetAccess facet_acess_server = this.Facet [ThisFacetInstance];

			foreach (KeyValuePair<int, FacetAccess> facet_access_client in this.Facet)
			{
				int facet_instance = facet_access_client.Key;
				if (facet_instance != this.ThisFacetInstance) 
				{
					string ip_address_client = facet_access_client.Value.ip_address;
					int port_client = facet_access_client.Value.port + this.ThisFacetInstance;
					IPHostEntry ipHostInfo_client = Dns.GetHostEntry (ip_address_client);
					IPAddress ipAddress_client = ipHostInfo_client.AddressList [0];
					IPEndPoint endPoint_client = new IPEndPoint (ipAddress_client, port_client);
					end_point_client [facet_instance] = endPoint_client;

					Console.WriteLine("CREATE SOCKETS - end_point_client[" + facet_instance + "]=" + endPoint_client);

					string ip_address_server = facet_acess_server.ip_address;
					int port_server = facet_acess_server.port + facet_instance;
					IPHostEntry ipHostInfo_server = Dns.GetHostEntry (ip_address_server);
					IPAddress ipAddress_server = ipHostInfo_server.AddressList [0];
					IPEndPoint endPoint_server = new IPEndPoint (ipAddress_server, port_server);
					end_point_server [facet_instance] = endPoint_server;

					Console.WriteLine("CREATE SOCKETS - end_point_server[" + facet_instance + "]=" + endPoint_server);

					// Create a TCP/IP client socket.
					Socket client_socket = new Socket (AddressFamily.InterNetwork, 
						SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

					// Create a TCP/IP server socket.
					Socket server_socket = new Socket (AddressFamily.InterNetwork, 
						SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

					server_socket.SendTimeout = server_socket.ReceiveTimeout = client_socket.SendTimeout = client_socket.ReceiveTimeout = -1;
					client_socket_facet [facet_instance] = client_socket;
					server_socket_facet [facet_instance] = server_socket;
				}
			}

			Thread thread_connect_sockets = new Thread (new ThreadStart (connectSockets));
			Thread thread_accept_sockets = new Thread (new ThreadStart (acceptSockets));

			thread_connect_sockets.Start();
			thread_accept_sockets.Start ();

			Console.WriteLine("CREATE_SOCKETS - connectSockets and acceptSockets launched !!!");

			thread_connect_sockets.Join ();
			thread_accept_sockets.Join ();

			Console.WriteLine("CREATE_SOCKETS - connectSockets and acceptSockets finished !!!");
		}			




		public void Send<T> (T value, Tuple<int,int> dest, int tag)
		{
			byte[] value_packet = ObjectToByteArray (value);
			handle_SEND (AliencommunicatorOperation.SEND, new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet));
		}

		public IRequest ImmediateSend<T> (T value, Tuple<int,int> dest, int tag)
		{
			ManualResetEvent waiting_request = new ManualResetEvent (false);
			IRequest request =  Request.createRequest(waiting_request, dest, tag);
			byte[] value_packet = ObjectToByteArray (value);

			Thread t = new Thread (new ThreadStart(delegate() {
				handle_SEND (AliencommunicatorOperation.SEND, new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet));
				waiting_request.Set();
			}));
            t.Start();

			return request;
		}

		public void Send<T> (T[] values, Tuple<int,int> dest, int tag)
		{
			byte[] value_packet = ObjectToByteArray (values);
			handle_SEND (AliencommunicatorOperation.SEND_ARRAY, new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet));
		}

		public IRequest ImmediateSend<T> (T[] values, Tuple<int,int> dest, int tag)
		{
			ManualResetEvent waiting_request = new ManualResetEvent (false);
			IRequest request =  Request.createRequest(waiting_request, dest, tag);
			byte[] value_packet = ObjectToByteArray (values);

			Thread t = new Thread (new ThreadStart(delegate() {
				handle_SEND (AliencommunicatorOperation.SEND_ARRAY, new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet));
				waiting_request.Set();
			}));
            t.Start();

			return request;
		}

		public IRequest ImmediateSyncSend<T> (T value, Tuple<int,int> dest, int tag)
		{
			// In SAFe, it has the same semantics of ImmediateSend, since the call returns after handle_SEND has been completed.
			return ImmediateSend<T> (value, dest, tag);
		}

		void handle_SEND (int operation_type, Tuple<int,int,int,byte[]> operation_info)
		{
			int facet_src = this.ThisFacetInstance;
			int facet_dst = operation_info.Item1;
			int src = 0;
			int dst = operation_info.Item2;
			int tag = operation_info.Item3;


			EnvelopType envelop = new EnvelopType (operation_type, facet_src, facet_dst, src, dst, tag);
			byte[] message1 = operation_info.Item4;

			if (tag >=0 /* tag */)
				synchronizer_monitor.clientSendRequest (envelop, message1);
			else
				synchronizer_monitor.clientSendRequestAnyTag (envelop, message1, ref tag);

		}

		//private object lock_recv = new object();

		public T Receive<T> (Tuple<int,int> source, int tag)
		{
			T result;
			Receive<T>(source, tag, out result);
			return result;
		}

		public void Receive<T> (Tuple<int,int> source, int tag, out T value)
		{
			br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status;
			Receive<T>(source, tag, out value, out status);
		}

		public void Receive<T> (Tuple<int,int> source, int tag, out T value, out br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status)
		{
			byte[] v = handle_RECEIVE(AliencommunicatorOperation.RECEIVE, new Tuple<int,int,int>(source.Item1, source.Item2, tag));
			value = (T) ByteArrayToObject(v);
			status = SAFeCompletedStatus.createStatus(source, tag);
		}

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.IReceiveRequest ImmediateReceive<T> (Tuple<int,int> source, int tag)
		{
			ManualResetEvent waiting_request = new ManualResetEvent (false);
			IReceiveRequest request =  ValueReceiveRequest<T>.createRequest(waiting_request, source, tag);

			Thread t = new Thread (new ThreadStart(delegate() {
				byte[] v = handle_RECEIVE(AliencommunicatorOperation.RECEIVE, new Tuple<int,int,int>(source.Item1, source.Item2, tag));
				waiting_request.Set();
				((ValueReceiveRequest<T>)request).SetValue(v);
			}));
            t.Start();

			return request;
		}

		public void Receive<T> (Tuple<int,int> source, int tag, ref T[] values)
		{
			br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status;
			Receive<T>(source, tag, ref values, out status);
		}

		public void Receive<T> (Tuple<int,int> source, int tag, ref T[] values, out br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status)
		{
			byte[] v = handle_RECEIVE(AliencommunicatorOperation.RECEIVE_ARRAY, new Tuple<int,int,int>(source.Item1, source.Item2, tag));
			values = (T[])ByteArrayToObject (v);
			status = SAFeCompletedStatus.createStatus(source, tag); 
		}

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.IReceiveRequest ImmediateReceive<T> (Tuple<int,int> source, int tag, T[] values)
		{
			ManualResetEvent waiting_request = new ManualResetEvent (false);
			IReceiveRequest request =  ValueReceiveRequest<T>.createRequest(waiting_request, source, tag);

			Thread t = new Thread (new ThreadStart(delegate() {
				byte[] v = handle_RECEIVE(AliencommunicatorOperation.RECEIVE_ARRAY, new Tuple<int,int,int>(source.Item1, source.Item2, tag));
				waiting_request.Set();
				((ValueReceiveRequest<T>)request).SetValue(v);
			}));
            t.Start();

			return request;
		}

		byte[] handle_RECEIVE (int operation_type, Tuple<int,int,int> operation_info)
		{
			int facet_src = this.ThisFacetInstance;
			int facet_dst = operation_info.Item1;
			int src = 0;
			int dst = operation_info.Item2;
			int tag = operation_info.Item3;

			EnvelopType envelop = new EnvelopType (operation_type, facet_src, facet_dst, src, dst, tag);
			byte[] message2 = tag < 0 ? synchronizer_monitor.clientSendRequestAnyTag (envelop, new byte[0], ref tag) 
				                      : synchronizer_monitor.clientSendRequest (envelop, new byte[0]);

			return message2;
		}


		#region IDisposable implementation
		private bool disposed = false;

		protected override void Dispose(bool disposing)
		{

			if (!disposed)
			{
				if (disposing)
				{
					Console.WriteLine("DISPOSING BINDING ROOT ...");
					foreach (int i in thread_receive_requests.Keys)
						//for (int i=0; i<thread_receive_requests.Count; i++)
						if (i != this.ThisFacetInstance)
							thread_receive_requests[i].Abort ();
				}
				base.Dispose (disposing);
			}
			//dispose unmanaged resources
			disposed = true;
		}

		#endregion

	}

	class SynchronizerMonitor
	{
		private int server_facet = default(int);
		private string instance_name = null;
		private IDictionary<int,Socket> client_socket_facet = new Dictionary<int,Socket>();
		private IDictionary<EnvelopKey, IDictionary<int,Queue<byte[]>>> reply_pending_list = new Dictionary<EnvelopKey,IDictionary<int,Queue<byte[]>>>();
		private IDictionary<EnvelopKey, IDictionary<int,Queue<AutoResetEvent>>> request_pending_list = new Dictionary<EnvelopKey,IDictionary<int,Queue<AutoResetEvent>>>();
		private IChannelImpl unit;

		private object sync = new object();

		public SynchronizerMonitor (IChannelImpl unit, IDictionary<int,Socket> client_socket_facet, int server_facet, string instance_name)
		{
			this.unit = unit;
			this.client_socket_facet = client_socket_facet;
			this.server_facet = server_facet;
			this.instance_name = instance_name;
		}

		public byte[] clientSendRequest(EnvelopType envelop, byte[] messageSide1)
		{
			EnvelopKey envelop_key = new EnvelopKey (envelop);
			int envelop_tag = envelop.Item6;
			Console.WriteLine(server_facet + "/" + ": clientSendRequest 1" + " / "  + envelop_key +" -- " + instance_name);

			byte[] messageSide2 = null;
			Monitor.Enter (sync);
			try
			{
				// envia a requisição para o root parceiro
				int facet = envelop.Item3;
				Console.WriteLine(server_facet + "/" + ": clientSendRequest send to facet " + facet + " - nofsockets=" + client_socket_facet.Count + " / "  + envelop_key+" -- " + instance_name);
				foreach (int f in client_socket_facet.Keys)
					Console.WriteLine(server_facet + "/" + ": clientSendRequest --- FACET KEY=" + f);
				Socket socket = client_socket_facet [facet];
				byte[] messageSide1_enveloped_raw = ObjectToByteArray (new Tuple<EnvelopType,byte[]> (envelop, messageSide1));
				Int32 length = messageSide1_enveloped_raw.Length;
				byte[] messageSide1_enveloped_raw_ = new byte[4 + length];
				BitConverter.GetBytes(length).CopyTo(messageSide1_enveloped_raw_,0);
				Array.Copy(messageSide1_enveloped_raw, 0, messageSide1_enveloped_raw_, 4, length);

				socket.Send (messageSide1_enveloped_raw_);

				Console.WriteLine(server_facet + "/" + ": clientSendRequest 2 nbytes=" + messageSide1_enveloped_raw.Length + " / "  + envelop_key);

				// Verifica se já há resposta para a requisição no "conjunto de respostas pendentes de requisição"
				if (!(reply_pending_list.ContainsKey (envelop_key) && reply_pending_list [envelop_key].ContainsKey(envelop_tag)) || 
					(reply_pending_list.ContainsKey (envelop_key) && reply_pending_list [envelop_key].ContainsKey(envelop_tag) && reply_pending_list [envelop_key][envelop_tag].Count == 0)) 
				{
					Console.WriteLine(server_facet + "/" + ": clientSendRequest 3 - BEFORE WAIT " + envelop_key);
					// Se não houver, coloca um item no "conjunto de requisições pendentes de resposta" e espera.

					if (!request_pending_list.ContainsKey(envelop_key))
						request_pending_list [envelop_key] = new Dictionary<int,Queue<AutoResetEvent>>();

					if (!request_pending_list [envelop_key].ContainsKey(envelop_tag))
					{
						request_pending_list [envelop_key][envelop_tag] = new Queue<AutoResetEvent>();
						request_pending_list [envelop_key][envelop_tag].Enqueue(new AutoResetEvent(false));
					}

					AutoResetEvent sync_send = request_pending_list [envelop_key][envelop_tag].Peek();

					//request_pending_list [envelop_key][envelop_tag] = sync_send;
					Monitor.Exit(sync);
					Console.WriteLine("clientSendRequest - WAIT / " + unit.CID.getInstanceName() + "/" + sync_send.GetHashCode()  + " BEFORE !!! " );
					sync_send.WaitOne();
					Console.WriteLine("clientSendRequest - WAIT / " + unit.CID.getInstanceName()  + "/" + sync_send.GetHashCode()  + " AFTER !!! " );
					Monitor.Enter(sync);
					Console.WriteLine(server_facet + "/" + ": clientSendRequest 3 - AFTER WAIT " + envelop_key);
				}
				Console.WriteLine(server_facet + "/" + ": clientSendRequest 4" + " / "  + envelop_key);

				Queue<byte[]> pending_replies = reply_pending_list [envelop_key][envelop_tag];
				Console.WriteLine(server_facet + "/" + ": clientSendRequest 5 -- pending_replies.Count = " + pending_replies.Count);
				if (pending_replies.Count > 0)
					messageSide2 = reply_pending_list[envelop_key][envelop_tag].Dequeue();

				if (pending_replies.Count == 0)
					reply_pending_list[envelop_key].Remove(envelop_tag);

				if (reply_pending_list[envelop_key].Count == 0)
					reply_pending_list.Remove(envelop_key);

				//reply_pending_list.Remove(envelop_key);
			}
			finally 
			{
				Monitor.Exit (sync);
			}

			Console.WriteLine(server_facet + "/" + ": clientSendRequest 5");
			// retorna a menagem ...
			return messageSide2;
		}

		public byte[] clientSendRequestAnyTag(EnvelopType envelop, byte[] messageSide1, ref int envelop_tag)
		{
			EnvelopKey envelop_key = new EnvelopKey (envelop);
			Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag 1" + " / "  + envelop_key +" -- " + instance_name);

			byte[] messageSide2 = null;
			Monitor.Enter (sync);
			try
			{
				// envia a requisição para o root parceiro
				int facet = envelop.Item3;
				Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag send to facet " + facet + " - nofsockets=" + client_socket_facet.Count + " / "  + envelop_key+" -- " + instance_name);
				foreach (int f in client_socket_facet.Keys)
					Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag --- FACET KEY=" + f);
				Socket socket = client_socket_facet [facet];
				byte[] messageSide1_enveloped_raw = ObjectToByteArray (new Tuple<EnvelopType,byte[]> (envelop, messageSide1));
				Int32 length = messageSide1_enveloped_raw.Length;
				byte[] messageSide1_enveloped_raw_ = new byte[4 + length];
				BitConverter.GetBytes(length).CopyTo(messageSide1_enveloped_raw_,0);
				Array.Copy(messageSide1_enveloped_raw, 0, messageSide1_enveloped_raw_, 4, length);

				socket.Send (messageSide1_enveloped_raw_);

				Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag 2 nbytes=" + messageSide1_enveloped_raw.Length + " / "  + envelop_key);

				// Verifica se já há resposta para a requisição no "conjunto de respostas pendentes de requisição"
				if (!reply_pending_list.ContainsKey (envelop_key))
				{
					Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag 3 - BEFORE WAIT " + envelop_key);
					// Se não houver, coloca um item no "conjunto de requisições pendentes de resposta" e espera.

					if (!request_pending_list.ContainsKey(envelop_key))
						request_pending_list [envelop_key] = new Dictionary<int,Queue<AutoResetEvent>>();

					if (!request_pending_list [envelop_key].ContainsKey(-1))
					{
						request_pending_list [envelop_key][-1] = new Queue<AutoResetEvent>();
						request_pending_list [envelop_key][-1].Enqueue(new AutoResetEvent(false));
					}

					AutoResetEvent sync_send = request_pending_list [envelop_key][-1].Peek();

					//request_pending_list [envelop_key][envelop_tag] = sync_send;
					Monitor.Exit(sync);
					Console.WriteLine("clientSendRequestAny - WAIT / " + unit.CID.getInstanceName() + "/" + sync_send.GetHashCode()  + " BEFORE !!! " );
					sync_send.WaitOne()	;
					Console.WriteLine("clientSendRequestAny - WAIT / " + unit.CID.getInstanceName()  + "/" + sync_send.GetHashCode()  + " AFTER !!! " );
					Monitor.Enter(sync);
					Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag 3 - AFTER WAIT " + envelop_key);
				}
				Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag 4" + " / "  + envelop_key);

				int[] keys_vector = new int[reply_pending_list[envelop_key].Keys.Count];
				reply_pending_list[envelop_key].Keys.CopyTo(keys_vector,0);

				envelop_tag = keys_vector[0];
				Queue<byte[]> pending_replies = reply_pending_list [envelop_key][envelop_tag];
				Console.WriteLine(server_facet + "/" + ": clientSendRequestAnyTag 5 -- pending_replies.Count = " + pending_replies.Count);
				if (pending_replies.Count > 0)
					messageSide2 = reply_pending_list[envelop_key][envelop_tag].Dequeue();

				if (pending_replies.Count == 0)
					reply_pending_list[envelop_key].Remove(envelop_tag);

				if (reply_pending_list[envelop_key].Count == 0)
					reply_pending_list.Remove(envelop_key);

				//reply_pending_list.Remove(envelop_key);
			}
			finally 
			{
				Monitor.Exit (sync);
			}

			Console.WriteLine(server_facet + "/" + ": clientSendRequest 5");
			// retorna a menagem ...
			return messageSide2;
		}

		private static int BUFFER_SIZE = 1024*1024;

		public void serverReceiveRequests(int facet, Socket server_socket)
		{
			byte[] buffer = new byte[BUFFER_SIZE];
			byte[] buffer2 = new byte[BUFFER_SIZE];

			int nbytes = default(int);

			Console.WriteLine("serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " BEFORE 1");
			nbytes = server_socket.Receive (buffer);		    
			Console.WriteLine("serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " AFTER 1");

			Console.WriteLine(server_facet + "/" + ": serverReceiveRequests 1 - RECEIVED " + nbytes + " bytes -- " + instance_name);

			if (nbytes == 0) 
			{
				string error_message = server_facet + "/" + ": serverReceiveRequests  -- the partner " + this.server_facet + " is died " + instance_name;
				Console.WriteLine(error_message);
				throw new Exception(error_message);
			}

			while (true)
			{
				int length = BitConverter.ToInt32 (buffer, 0);
				nbytes = nbytes - 4;
				byte[] message = new byte[length];

				Console.WriteLine(server_facet + "/" + ": serverReceiveRequests 2 - length is " + length + " bytes" + " / nbytes = " + nbytes);

				while (nbytes < length) 
				{
					int nbytes2 = server_socket.Receive (buffer2);
					Console.WriteLine(server_facet + "/" + ": serverReceiveRequests 2 - LOOP - length is " + length + " bytes" + " / nbytes2 = " + nbytes2 + " / nbytes = " + nbytes);
					Array.Copy (buffer2, 0, buffer, nbytes + 4, nbytes2);
					nbytes += nbytes2;
				}

				Console.WriteLine(server_facet + "/" + ": serverReceiveRequests 2 - AFTER LOOP - length is " + length + " bytes" + " / nbytes = " + nbytes);

				Array.Copy(buffer, 4, message, 0, length);
				requestQueue.Add (message);

				if (nbytes == length) 
				{
					Console.WriteLine("serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " BEFORE 2");
					nbytes = server_socket.Receive (buffer);
					Console.WriteLine("serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " AFTER 2");
					Console.WriteLine(server_facet + "/" + ": serverReceiveRequests 3 - RECEIVED " + nbytes + " bytes --- " + instance_name);

					if (nbytes == 0) 
					{
						string error_message = server_facet + "/" + ": serverReceiveRequests  -- the partner " + this.server_facet + " is died " + instance_name;
						Console.WriteLine(error_message);
						throw new Exception(error_message);
					}
				} 
				else if (nbytes > length) 
				{ 
					// assume that nbytes - length > 4
					byte[] aux = buffer;
					nbytes = nbytes - length;

					Array.Copy(buffer, length + 4, buffer2, 0, nbytes);
					buffer = buffer2;
					buffer2 = aux;
					Console.WriteLine(server_facet + "/" + ": serverReceiveRequests 4 - nbytes=" + nbytes);
				}
			}
		}

		private ProducerConsumerQueue<byte[]> requestQueue = new ProducerConsumerQueue<byte[]>();

		public void serverReadRequest() 
		{
			Console.WriteLine(server_facet + "/" + ": serverReadRequest 1 ");

			byte[] buffer =	requestQueue.Take ();
			int nbytes =  buffer.Length;

			Console.WriteLine(server_facet + "/" + ": serverReadRequest 2 " + nbytes + " bytes received.");

			Monitor.Enter (sync);
			try
			{
				// Aguarda uma resposta proveniente do outro root parceiro.
				byte[] messageSide1_enveloped_raw = new byte[nbytes];
				Console.WriteLine(server_facet + "/" + ": serverReadRequest 2-1 nbytes=" + nbytes);

				// TODO: otimizar isso ...
				//for (int i=0; i<nbytes; i++)
				//	messageSide1_enveloped_raw[i] = buffer[i];

				Array.Copy(buffer,0,messageSide1_enveloped_raw,0,nbytes);

				Tuple<EnvelopType,byte[]> messageSide1_enveloped = (Tuple<EnvelopType,byte[]>) ByteArrayToObject (messageSide1_enveloped_raw);

				EnvelopType envelop = messageSide1_enveloped.Item1;
				EnvelopKey envelop_key = new EnvelopKey (envelop);
				int envelop_tag = envelop.Item6;

				// Coloca a resposta no "conjunto de respostas pendentes de requisição"
				if (!reply_pending_list.ContainsKey(envelop_key))
					reply_pending_list [envelop_key] = new Dictionary<int,Queue<byte[]>>();

				if (!reply_pending_list [envelop_key].ContainsKey(envelop_tag))
					reply_pending_list [envelop_key][envelop_tag] = new Queue<byte[]>();

				reply_pending_list [envelop_key][envelop_tag].Enqueue(messageSide1_enveloped.Item2);

				Console.WriteLine(server_facet + "/" + ": serverReadRequest 3 " + envelop.Item1 + "," +  envelop_key);
				foreach (EnvelopKey ek in request_pending_list.Keys) 
					Console.WriteLine(server_facet + ": key: " + ek);

				// Busca, no "conjunto de requisições pendentes de resposta", a requisição correspondente a resposta.
				if (request_pending_list.ContainsKey (envelop_key) && request_pending_list[envelop_key].ContainsKey(envelop_tag)) 
				{
					Console.WriteLine(server_facet + "/" + ": serverReadRequest 3-1" + " / "  + envelop_key);
					AutoResetEvent sync_send = request_pending_list[envelop_key][envelop_tag].Dequeue();

					sync_send.Set();

					if (request_pending_list[envelop_key][envelop_tag].Count == 0)
						request_pending_list[envelop_key].Remove(envelop_tag);

					if (request_pending_list[envelop_key].Count==0)
						request_pending_list.Remove(envelop_key);

					Console.WriteLine(server_facet + "/" + ": serverReadRequest 3-2"+ " / "  + envelop_key) ;
				} 
				else if (request_pending_list.ContainsKey (envelop_key) && request_pending_list[envelop_key].ContainsKey(-1)) 
				{
					Console.WriteLine(server_facet + "/" + ": serverReadRequest 3-1" + " / "  + envelop_key);
					AutoResetEvent sync_send = request_pending_list[envelop_key][-1].Dequeue();
					//Monitor.Pulse (sync_send);
					sync_send.Set();

					if (request_pending_list[envelop_key][-1].Count == 0)
						request_pending_list[envelop_key].Remove(-1);

					if (request_pending_list[envelop_key].Count==0)
						request_pending_list.Remove(envelop_key);

					Console.WriteLine(server_facet + "/" + ": serverReadRequest 3-2"+ " / "  + envelop_key) ;
				}  
			}
			finally 
			{
				Monitor.Exit (sync);
			}
			Console.WriteLine(server_facet + "/" + ": serverReadRequest 4");
		}

		// Convert an object to a byte array
		private static byte[] ObjectToByteArray(Object obj)
		{
			if(obj == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream ms = new MemoryStream();
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}

		// Convert a byte array to an Object
		private static Object ByteArrayToObject(byte[] arrBytes)
		{
			MemoryStream memStream = new MemoryStream();
			BinaryFormatter binForm = new BinaryFormatter();
			memStream.Write(arrBytes, 0, arrBytes.Length);
			memStream.Seek(0, SeekOrigin.Begin);
			Object obj = (Object) binForm.Deserialize(memStream);
			return obj;
		}
	}

	class EnvelopKey
	{
		private EnvelopType envelop = null;
		public EnvelopKey(EnvelopType envelop)
		{
			this.envelop = envelop;
		}

		public override string ToString ()
		{
			string key=base.ToString();
			switch (envelop.Item1) {
			case AliencommunicatorOperation.SEND:
			case AliencommunicatorOperation.SYNC_SEND:
			case AliencommunicatorOperation.SEND_ARRAY:
				//				key = string.Format ("SR-{0}-{1}-{2}-{3}-{4}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5, envelop.Item6);
				key = string.Format ("SR-{0}-{1}-{2}-{3}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5);
				break;
			case AliencommunicatorOperation.RECEIVE:
			case AliencommunicatorOperation.RECEIVE_ARRAY:
				//				key = string.Format ("SR-{1}-{0}-{3}-{2}-{4}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5, envelop.Item6);
				key = string.Format ("SR-{1}-{0}-{3}-{2}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5);
				break;
			case AliencommunicatorOperation.PROBE:
				break;
			case AliencommunicatorOperation.ALL_GATHER:
				break;
			case AliencommunicatorOperation.ALL_GATHER_FLATTENED:
				break;
			case AliencommunicatorOperation.ALL_REDUCE:
				break;
			case AliencommunicatorOperation.ALL_REDUCE_ARRAY:
				break;
			case AliencommunicatorOperation.ALL_TO_ALL:
				break;
			case AliencommunicatorOperation.ALL_TO_ALL_FLATTENED:
				break;
			case AliencommunicatorOperation.REDUCE_SCATTER:
				break;
			case AliencommunicatorOperation.BROADCAST:
				break;
			case AliencommunicatorOperation.BROADCAST_ARRAY:
				break;
			case AliencommunicatorOperation.SCATTER:
				break;
			case AliencommunicatorOperation.SCATTER_FROM_FLATTENED:
				break;
			case AliencommunicatorOperation.GATHER:
				break;
			case AliencommunicatorOperation.GATHER_FLATTENED:
				break;
			case AliencommunicatorOperation.REDUCE:
				break;
			case AliencommunicatorOperation.REDUCE_ARRAY:
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}

			return key;
		}

		public override bool Equals(object obj)
		{
			EnvelopKey fooItem = obj as EnvelopKey;

			return fooItem.ToString().Equals(this.ToString());
		}

		public override int GetHashCode ()
		{
			return this.ToString().GetHashCode();
		}

	}

	public class ProducerConsumerQueue<T> : BlockingCollection<T>
	{
		/// <summary>
		/// Initializes a new instance of the ProducerConsumerQueue, Use Add and TryAdd for Enqueue and TryEnqueue and Take and TryTake for Dequeue and TryDequeue functionality
		/// </summary>
		public ProducerConsumerQueue()  
			: base(new ConcurrentQueue<T>())
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProducerConsumerQueue, Use Add and TryAdd for Enqueue and TryEnqueue and Take and TryTake for Dequeue and TryDequeue functionality
		/// </summary>
		/// <param name="maxSize"></param>
		public ProducerConsumerQueue(int maxSize)
			: base(new ConcurrentQueue<T>(), maxSize)
		{
		}



	}

}

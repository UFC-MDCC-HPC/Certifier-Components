using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeSWC2;
using System.Collections.Generic;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortSWC2;


namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl
{
	public class IVerifyClientPortSWC2Impl: BaseIVerifyClientPortSWC2Impl, IVerifyClientPortSWC2
	{ 
		public override void main()
		{
		}
	
		public override void after_initialize ()
		{
			Channel.TraceFlag = true;

			foreach (KeyValuePair<int,IDictionary<string,int>> t in this.UnitSizeInFacet)
				foreach (KeyValuePair<string,int> u in t.Value)
					Console.WriteLine ("SIZE OF UNIT {1} in FACET {0} is {2}", t.Key, u.Key, u.Value);			;

			int server_size = this.UnitSizeInFacet[this.FacetIndexes [FACET_SERVER] [0]]["server"];

			this.client = new IVerifyDataPortClientTypeSWC2Impl (server_size, Channel);
		}

		private IVerifyDataPortClientTypeSWC2 client = null;
		public IVerifyDataPortClientTypeSWC2 Client { get {	return client; } }

		internal class IVerifyDataPortClientTypeSWC2Impl : IVerifyDataPortClientTypeSWC2
		{
			public int  operation_tag = 1 ;

			public int dataCertifierTactical = 71; 

			//SWC2
			public int number_units_tactical;
			public int [] number_properties_per_unit_tactical;
			public int slice_size;
			public int[] properties_status;
			public int []index_first_prop_tact;
			public int num_properties;
			public string [] property_files;

			int num_properties_aux;
			int number_prop_read;
			string[] arr ;

			private IChannel channel;
			private IChannel Channel { get { return channel; } }
			private int server_size = 0;
			//SWC2

			public IVerifyDataPortClientTypeSWC2Impl(int server_size, IChannel channel)
			{
				this.server_size = server_size;
				this.channel = channel;
			}

			public void setNumProperties(int numProperties)
			{
				Console.WriteLine ("ENTER IVerifyClientPortSWC2Impl - setNumProperties");

				this.num_properties = numProperties;

				number_units_tactical = server_size;
				number_properties_per_unit_tactical = new int[number_units_tactical];
				slice_size = (int) Math.Floor ((double)num_properties / number_units_tactical);
				num_properties_aux = num_properties;

				for (int i = 0; i < number_units_tactical; i++) 
				{
					number_properties_per_unit_tactical [i] = slice_size;
					num_properties_aux -= slice_size;
				}

				int index=0;
				for (int i = 0; i < num_properties_aux; i++) 
				{
					number_properties_per_unit_tactical [index++] += 1;
					if (index == number_units_tactical)
						index = 0;
				}
				num_properties_aux = 0;

				arr = new string[slice_size + 1];

				properties_status = new int[slice_size + 1];
				index_first_prop_tact = new int[number_units_tactical];
				number_prop_read = 0;

				Console.WriteLine ("EXIT VerifyClientPortSWC2Impl - setNumProperties");
			}

			Tuple<int,int> target_server_unit(int u)
			{
				return new Tuple<int, int> (FACET_SERVER, u);	
			}

			public void setMcrl2File(string mCRL2_file)
			{
				Console.WriteLine ("ENTER IVerifyClientPortSWC2Impl - setMcrl2File");

				RequestList reqList = new RequestList ();
				for (int s = 0; s < server_size; s++) 
				{
					Channel.Send<int> (0, target_server_unit (s), operation_tag);
					Request req = Channel.ImmediateSyncSend<string> (mCRL2_file, target_server_unit (s), dataCertifierTactical);
					reqList.Add (req);
				}
				reqList.WaitAll ();

				Console.WriteLine ("EXIT IVerifyClientPortSWC2Impl - setMcrl2File");
			}

			public void setPropertyFiles(string [] property_files)
			{
				Console.WriteLine ("ENTER IVerifyClientPortSWC2Impl - setPropertyFiles");
				this.property_files = property_files;
				Console.WriteLine ("EXIT IVerifyClientPortSWC2Impl - setPropertyFiles");
			}

			public void setNumPropsTacticals()
			{
				Console.WriteLine ("ENTER IVerifyClientPortSWC2Impl - setNumPropsTacticals *** number_units_tactical = {0}", number_units_tactical);

				RequestList reqList = new RequestList ();
				for (int i = 0; i < number_units_tactical; i++) 
				{
					Channel.Send<int> (1,  target_server_unit (i), operation_tag);
					Console.WriteLine ("IVerifyClientPortSWC2Impl - setNumPropsTacticals *** number_properties_per_unit_tactical [{0}] = {1}", i, number_properties_per_unit_tactical [i]);
					Request req = Channel.ImmediateSyncSend<int> (number_properties_per_unit_tactical [i],  target_server_unit (i), dataCertifierTactical);
					reqList.Add (req);
				}
				reqList.WaitAll ();

				Console.WriteLine ("EXIT IVerifyClientPortSWC2Impl - setNumPropsTacticals");

			}

			public void setIndexFirstPropTacticals()
			{
				Console.WriteLine ("ENTER IVerifyClientPortSWC2Impl - setIndexFirstPropTacticals");

				RequestList reqList = new RequestList ();
				for (int i = 0; i < number_units_tactical; i++) 
				{
					Channel.Send<int> (2, target_server_unit (i), operation_tag);
					Request req = Channel.ImmediateSyncSend<int> (number_prop_read, target_server_unit (i), dataCertifierTactical);
					reqList.Add (req);
					index_first_prop_tact [i] = number_prop_read;
					number_prop_read += number_properties_per_unit_tactical [i];
				}
				reqList.WaitAll ();

				Console.WriteLine ("EXIT IVerifyClientPortSWC2Impl - setIndexFirstPropTacticals");
			}

			public void setPropertiesTacticals()
			{
				Console.WriteLine ("ENTER IVerifyClientPortSWC2Impl - setPropertiesTacticals");
				RequestList reqList = new RequestList ();

				for (int i = 0; i < number_units_tactical; i++) 
				{
					Array.Copy (property_files, index_first_prop_tact [i], arr, 0, number_properties_per_unit_tactical [i]);

					Channel.Send<int> (3, target_server_unit (i), operation_tag);
					Request req = Channel.ImmediateSyncSend<string[]>(arr, target_server_unit (i), dataCertifierTactical);
					reqList.Add (req);
				}

				reqList.WaitAll ();

				Console.WriteLine ("EXIT IVerifyClientPortSWC2Impl - setPropertiesTacticals");
			}
		}
	}
}

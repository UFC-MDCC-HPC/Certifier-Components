using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using System.IO;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortImpl
{
	public class IVerifyServerPort : 
	BaseIVerifyServerPort, 
	IVerifyServerPort<IVerifyDataPortServerType>
	{   
		public int certifier = 0; 
		public string mCRL2_file;
		public int dataCertifierTactical = 71; 
		public string []property_files; 
		public override void main()
		{  
			while (true) {
				setMcrl2File ();
				setNumProperties ();
				setIndexMyFirstProp ();
				setPropertyFiles();

			}

		
		}
	
		public void setMcrl2File(){
			channel.Broadcast<string>(ref mCRL2_file,certifier);
			service.setMcrl2File(ref mCRL2_file);
		}
		void setNumProperties(){

			service.setNumProperties(channel.Receive<int>(certifier,dataCertifierTactical));
		}
		void setIndexMyFirstProp(){

			service.setIndexMyFirstProp(channel.Receive<int>(certifier,dataCertifierTactical));
		}

		void setPropertyFiles(){
			channel.Receive<string>(certifier,dataCertifierTactical, ref property_files);
			service.setPropertyFiles (ref property_files);
		
		}





		private IVerifyDataPortServerType service;

		public IVerifyDataPortServerType Server {
			set {
				this.service = value;
			}
		}


		public MPI.Intercommunicator channel 
		{set { channel = value;} get {return channel;}}

		public override void after_initialize ()
		{
			int remote_leader = this.UnitRanks ["client"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);
		}




	}
}



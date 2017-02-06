using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortImpl
{
	public class IVerifyServerPort : 
	BaseIVerifyServerPort, 
	IVerifyServerPort<IVerifyDataPortServerType>
	{
		public override void main()
		{
		}
	

		 
		public int certifier = 0; 
		public int dataCertifierTactical = 71; 
		public MPI.Intercommunicator channel 
		{set { channel = value;} get {return channel;}}

		void getmCRL2FileContent(ref string mCRL2_file){


			channel.Broadcast<string>(ref mCRL2_file,certifier);
		}
		int getNumProperties(){return 
			channel.Receive<int>(certifier,dataCertifierTactical);}
		int getIndexMyFirstProp(){
			return channel.Receive<int>(certifier,dataCertifierTactical);}
		void getProperties (ref string [] property_files){
			
			channel.Receive<string>(certifier,dataCertifierTactical, ref property_files);}
	
	

		public override void after_initialize ()
		{
			int remote_leader = this.UnitRanks ["client"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);
		}




	}
}



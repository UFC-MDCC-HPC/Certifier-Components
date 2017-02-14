using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;



namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortImpl
{
	public class IVerifyClientPort : BaseIVerifyClientPort, IClientBase
	<IVerifyDataPortClientType>
	{   public int certifier = 0; 
		public int operation, operation_tag = 1 ;

		public int dataCertifierTactical = 71; 
	

		public override void main()
		{ 
			
			
		}

		public int getRemoteSize(){
			return channel.RemoteSize;
		
		}

		public void setMcrl2File(ref string mCRL2_file){
			for (int s = 0; s < channel.RemoteSize; s++) {
				channel.Send<string>(mCRL2_file, s, dataCertifierTactical);
			}


		}
		void setNumProperties(int destination, int numProperties){


			channel.Send<int>(numProperties, destination, dataCertifierTactical);

		}
		void setIndexMyFirstProp(int destination, int indexMyFirstProp){


			channel.Send<int>(indexMyFirstProp, destination, dataCertifierTactical);

		}

		void setPropertyFiles(int destination, ref string [] property_files){
			
			channel.Send<string>(property_files, destination, dataCertifierTactical);


		}



		public MPI.Intercommunicator channel 
		{set { channel = value;} get {return channel;}}

		public override void after_initialize ()
		{
			int remote_leader = this.UnitRanks ["server"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);

	     }


	}
}

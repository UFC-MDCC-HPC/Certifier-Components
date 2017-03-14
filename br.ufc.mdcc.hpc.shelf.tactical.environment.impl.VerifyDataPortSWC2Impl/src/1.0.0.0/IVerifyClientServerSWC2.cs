using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;
namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl
{
	public class IVerifyClientServerSWC2 : 
	BaseIVerifyClientServerSWC2, 
	IVerifyServerPort<IVerifyDataPortServerTypeSWC2>
	{
		public int certifier = 0; 
		public int operation, operation_tag = 0 ;

		public int dataCertifierTactical = 71; 

		//swc2
		public string mCRL2_file;
		public string []property_files; 




		public override void main()
		{  
			while (true) {


				operation = channel.Receive<int> (certifier, operation_tag);
				switch(operation){
				//SWC2
				case 0:
					setMcrl2File ();
					break;

				case 1:	setNumProperties ();
					break;

				case 2:setIndexMyFirstProp ();
					break;

				case 3: setPropertyFiles();
					break;



				}





			}


		}
		//swc2
		public void setMcrl2File(){
			mCRL2_file = channel.Receive<string>(certifier, dataCertifierTactical);
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




		private IVerifyDataPortServerTypeSWC2 service;

		public IVerifyDataPortServerTypeSWC2 Server {
			set {
				this.service = value;
			}
			get { return service; }
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

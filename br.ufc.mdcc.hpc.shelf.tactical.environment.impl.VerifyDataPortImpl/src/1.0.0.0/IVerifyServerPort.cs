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
		public int operation, operation_tag = 1 ;

		public int dataCertifierTactical = 71; 

		//swc2
		public string mCRL2_file;

		public string []property_files; 

	//c4
		public int[] num_units_program;
		public string[] args_programs;
		public string[] programs;


		public override void main()
		{  
			while (true) {
				

				operation = channel.Receive<int> (certifier, operation_tag);
				switch(operation){

				case 0:
					setMcrl2File ();
					break;
 					
				case 1:	setNumProperties ();
					break;
					
				case 2:setIndexMyFirstProp ();
					break;
					
				case 3: setPropertyFiles();
					break;

				case 4: setNumProgs();
					break;

				case 5: setUnitsProgs();
					break;

				case 6: setArgsProgs();
					break;

				case 7: setProgs();
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


		//c4


		void setNumProgs (){

			service.setNumProgs(channel.Receive<int>(certifier,dataCertifierTactical));
		}
		void setUnitsProgs (){
			channel.Receive<int>(certifier,dataCertifierTactical, ref num_units_program);
			service.setUnitsProgs (ref num_units_program);


		}
		void setArgsProgs (){

			channel.Receive<string>(certifier,dataCertifierTactical, ref args_programs);
			service.setArgsProgs (ref args_programs);

		}
		void setProgs (){	
			channel.Receive<string>(certifier,dataCertifierTactical, ref programs);
			service.setProgs (ref programs);
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



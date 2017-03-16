using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortC4Impl
{
	public class IVerifyServerPortC4 : BaseIVerifyServerPortC4, IVerifyServerPort<IVerifyDataPortServerTypeC4>
	{public int certifier = 0; 
		public int operation, operation_tag = 0 ;

		public int dataCertifierTactical = 71; 

		//c4
		public int[] num_units_program;
		public string[] args_programs;
		public string[] programs;
		public override void main()
		{  
			while (true) {


				operation = channel.Receive<int> (certifier, operation_tag);
				switch(operation){
			


					//C4
				case 0: setNumProgs();
					break;

				case 1: setUnitsProgs();
					break;

				case 2: setArgsProgs();
					break;

				case 3: setProgs();
					break;


				}





			}


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
	


		private IVerifyDataPortServerTypeC4 service;

		public IVerifyDataPortServerTypeC4 Service {
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

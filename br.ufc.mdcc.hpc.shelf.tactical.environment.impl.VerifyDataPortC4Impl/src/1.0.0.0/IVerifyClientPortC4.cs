using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeC4;
namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortC4Impl
{
	public class IVerifyClientPortC4 : BaseIVerifyClientPortC4, IClientBase<IVerifyDataPortClientTypeC4>
	{
		public int certifier = 0; 
		public int  operation_tag = 1 ;

		public int dataCertifierTactical = 71; 
		//C4

		public int num_programs;
		public string []programs;
		public string[] args_programs;
		public int []num_units_program;

		//public static string[] arr_transfer_prop;
		public  string[] arr_transfer_tac;
		public  int[] arr_int_transfer_tac;
		public int slice_size_tactical;
		public int []index_first_prog_unit_tactical;
		public int [] number_progs_per_unit_tactical;

		public int number_units_tactical;
		public override void main()
		{
		}
		//c4



		void setNumProgs (int number){

			num_programs = number;



			number_units_tactical = channel.RemoteSize;

			int num_progs_aux; int index; 

			slice_size_tactical = (int) Math.Floor( (double)num_programs / number_units_tactical);
			num_progs_aux = num_programs;
			for (int j = 0; j < number_units_tactical; j++) {
				number_progs_per_unit_tactical[j] = slice_size_tactical;
				num_progs_aux = num_progs_aux - slice_size_tactical;
			}
			index=0;
			for(int j=0 ; j< num_progs_aux;j++){
				number_progs_per_unit_tactical[index++]+= 1;
				if (index == number_units_tactical){
					index = 0;

				}

			}








			for (int j = 0; j < number_units_tactical; j++) {

				channel.Send<int> (4, j, operation_tag);
				channel.Send<int> (number_progs_per_unit_tactical [j], j, dataCertifierTactical);

			}

			arr_transfer_tac = new string[slice_size_tactical + 1];
			arr_int_transfer_tac = new int[slice_size_tactical + 1];
			index_first_prog_unit_tactical=new int[number_units_tactical];
			//channel.Send<int>(number, destination, dataCertifierTactical);

		}
		void setUnitsProgs (ref int[] num_units_program){
			this.num_units_program = num_units_program;
			//channel.Send<int>(num_units_program, destination, dataCertifierTactical);

			int number_prog_read = 0;





			for (int j = 0; j < number_units_tactical; j++) {



				index_first_prog_unit_tactical [j] = number_prog_read;
				/*	Console.WriteLine (" i " + i + " j " + j + " tactical_first_unit[i]+j " + (tactical_first_unit [i] + j)
						+ " number_progs_per_unit_tactical [i] " + number_progs_per_unit_tactical [i][j] +
						" index_first_prog_unit_tactical [i][j] " + index_first_prog_unit_tactical [i] [j]
						+" number_prog_read " +number_prog_read);
*/
				Console.WriteLine ("proximo2");

				Array.Copy (num_units_program, index_first_prog_unit_tactical[j], arr_int_transfer_tac, 0, number_progs_per_unit_tactical [j]);
				for (int k = 0; k < number_progs_per_unit_tactical [j]; k++) {
					Console.WriteLine ("unities of programs to " + j);
					Console.WriteLine (arr_int_transfer_tac [k]);
				}

				channel.Send<int> (5, j, operation_tag);
				channel.Send<int>(arr_int_transfer_tac, j, dataCertifierTactical);

				number_prog_read += number_progs_per_unit_tactical [j];

			}
		}
		void setArgsProgs (ref string[] args_programs){
			this.args_programs = args_programs;

			for (int j = 0; j < number_units_tactical; j++) {






				Array.Copy (args_programs, index_first_prog_unit_tactical[j], arr_transfer_tac, 0, number_progs_per_unit_tactical [j]);
				for (int k = 0; k < number_progs_per_unit_tactical [j]; k++) {
					Console.WriteLine ("args");
					Console.WriteLine (arr_transfer_tac [k]);
				}
				//Communicator.world.Send<string> (arr_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);
				channel.Send<int> (6, j, operation_tag);
				channel.Send<string>(arr_transfer_tac, j, dataCertifierTactical);



			}
			//channel.Send<string>(args_programs, destination, dataCertifierTactical);

		}
		void setProgs (ref string[] programs){
			this.programs = programs;

			for (int j = 0; j < number_units_tactical; j++) {






				Array.Copy (programs, index_first_prog_unit_tactical[j], arr_transfer_tac, 0, number_progs_per_unit_tactical [j]);

				for (int k = 0; k < number_progs_per_unit_tactical [j]; k++) {
					Console.WriteLine ("progs");
					Console.WriteLine (arr_transfer_tac[k]);
				}
				Console.WriteLine ("proximo");

				channel.Send<int> (7, j, operation_tag);
				channel.Send<string>(arr_transfer_tac, j, dataCertifierTactical);



			}
			//channel.Send<string>(programs, destination, dataCertifierTactical);

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

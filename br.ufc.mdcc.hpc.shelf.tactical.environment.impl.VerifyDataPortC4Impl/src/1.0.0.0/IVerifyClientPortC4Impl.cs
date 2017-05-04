using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeC4;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortC4;


namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortC4Impl
{
	public class IVerifyClientPortC4Impl : BaseIVerifyClientPortC4Impl, IVerifyClientPortC4
	{
		private int server_size = 0;

		public override void main()
		{
		}
		//c4

		public override void after_initialize ()
		{
			this.server_size = this.UnitSizeInFacet[this.FacetIndexes [FACET_SERVER] [0]]["server"];
			this.client = new IVerifyDataPortClientTypeC4Impl (server_size, Channel);
		}

		private IVerifyDataPortClientTypeC4 client = null;
		public IVerifyDataPortClientTypeC4 Client { get {	return client; } }

		internal class IVerifyDataPortClientTypeC4Impl : IVerifyDataPortClientTypeC4
		{
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

			private IChannel channel;
			private IChannel Channel { get { return channel; } }
			private int server_size = 0;

			public IVerifyDataPortClientTypeC4Impl(int server_size, IChannel channel)
			{
				this.server_size = server_size;
				this.channel = channel;
			}

			Tuple<int,int> source_client_unit(int u)
			{
				return new Tuple<int, int> (FACET_SERVER, u);	
			}

			public void setNumProgs (int number)
			{
				num_programs = number;

				number_units_tactical = server_size;

				int num_progs_aux; int index; 

				slice_size_tactical = (int) Math.Floor( (double)num_programs / number_units_tactical);
				num_progs_aux = num_programs;

				for (int j = 0; j < number_units_tactical; j++) 
				{
					number_progs_per_unit_tactical [j] = slice_size_tactical;
					num_progs_aux = num_progs_aux - slice_size_tactical;
				}

				index=0;
				for(int j=0 ; j< num_progs_aux;j++)
				{
					number_progs_per_unit_tactical[index++]+= 1;
					if (index == number_units_tactical)
						index = 0;
				}

				for (int j = 0; j < number_units_tactical; j++) 
				{
					Channel.Send<int> (4, source_client_unit(j), operation_tag);
					Channel.Send<int> (number_progs_per_unit_tactical [j], source_client_unit(j), dataCertifierTactical);
				}

				arr_transfer_tac = new string[slice_size_tactical + 1];
				arr_int_transfer_tac = new int[slice_size_tactical + 1];
				index_first_prog_unit_tactical=new int[number_units_tactical];
			}

			public void setUnitsProgs (ref int[] num_units_program)
			{
				this.num_units_program = num_units_program;
				//channel.Send<int>(num_units_program, destination, dataCertifierTactical);

				int number_prog_read = 0;

				for (int j = 0; j < number_units_tactical; j++) 
				{
					index_first_prog_unit_tactical [j] = number_prog_read;
					/*	Console.WriteLine (" i " + i + " j " + j + " tactical_first_unit[i]+j " + (tactical_first_unit [i] + j)
							+ " number_progs_per_unit_tactical [i] " + number_progs_per_unit_tactical [i][j] +
							" index_first_prog_unit_tactical [i][j] " + index_first_prog_unit_tactical [i] [j]
							+" number_prog_read " +number_prog_read);
	*/
					Console.WriteLine ("proximo2");

					Array.Copy (num_units_program, index_first_prog_unit_tactical[j], arr_int_transfer_tac, 0, number_progs_per_unit_tactical [j]);
				
					for (int k = 0; k < number_progs_per_unit_tactical [j]; k++) 
					{
						Console.WriteLine ("unities of programs to " + j);
						Console.WriteLine (arr_int_transfer_tac [k]);
					}

					Channel.Send<int> (5, source_client_unit(j), operation_tag);
					Channel.Send<int>(arr_int_transfer_tac, source_client_unit(j), dataCertifierTactical);

					number_prog_read += number_progs_per_unit_tactical [j];
				}
			}

			public void setArgsProgs (ref string[] args_programs)
			{
				this.args_programs = args_programs;

				for (int j = 0; j < number_units_tactical; j++) 
				{
					Array.Copy (args_programs, index_first_prog_unit_tactical[j], arr_transfer_tac, 0, number_progs_per_unit_tactical [j]);
				
					for (int k = 0; k < number_progs_per_unit_tactical [j]; k++) 
					{
						Console.WriteLine ("args");
						Console.WriteLine (arr_transfer_tac [k]);
					}

					//Communicator.world.Send<string> (arr_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);
					Channel.Send<int> (6, source_client_unit(j), operation_tag);
					Channel.Send<string>(arr_transfer_tac, source_client_unit(j), dataCertifierTactical);
				}
				//channel.Send<string>(args_programs, destination, dataCertifierTactical);
			}

			public void setProgs (ref string[] programs)
			{
				this.programs = programs;

				for (int j = 0; j < number_units_tactical; j++) 
				{
					Array.Copy (programs, index_first_prog_unit_tactical[j], arr_transfer_tac, 0, number_progs_per_unit_tactical [j]);

					for (int k = 0; k < number_progs_per_unit_tactical [j]; k++) 
					{
						Console.WriteLine ("progs");
						Console.WriteLine (arr_transfer_tac [k]);
					}

					Console.WriteLine ("proximo");

					Channel.Send<int> (7, source_client_unit(j), operation_tag);
					Channel.Send<string>(arr_transfer_tac, source_client_unit(j), dataCertifierTactical);
				}
				//channel.Send<string>(programs, destination, dataCertifierTactical);

			}
		}	
	}
}

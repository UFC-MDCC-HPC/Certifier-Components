using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ParTypes;
using System.Diagnostics;
using br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortC4Impl;
using  br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using MPI;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;

namespace br.ufc.mdcc.hpc.shelf.certify.tactical.impl.ParTypesImpl
{
	public class IParTypesImpl : BaseIParTypesImpl, IParTypes
	{
		public string []programs; 
		public int num_programs; 
		//public string[] args_programs;
		//public int []num_units_program;
		public int num_threads = 1;
		public int number_thread=0; 
		public int number_progs_consumed=0;
		public bool verification_is_inconclusive = false;
		public int number_programs_consumed=0;
		public int [] num_programs_thread;
		//public string [][] programs_properties_status;
		public string host="partypes.ddns.net";
		public int port = 9998;
		public int verification_status;
		//public string []property_names;
		
		public Boolean status_verification_properties;
		public override void main()
		{

		
			while(true){
				Verify.invoke (IVerify.VERIFY_PERFORM);
				invoke_verify_perform ();
				if (!verification_is_inconclusive) {
					status_verification_properties = this.Communicator.Allreduce(false, Operation<Boolean>.LogicalAnd);
				} else {
					status_verification_properties = this.Communicator.Allreduce(true, Operation<Boolean>.LogicalAnd);
				}
				if	(status_verification_properties) Verify.invoke (IVerify.VERIFY_CONCLUSIVE);
				else Verify.invoke (IVerify.VERIFY_INCONCLUSIVE);
			}
		}
		public void invoke_verify_perform(){

			//	Communicator.world.Receive<int>(certifier,verify_perform);
			verify_ ();

		}

		/*void setNumProgs(int number){
			path =System.Environment.GetEnvironmentVariable("PATH_TAC_ISP_EXEC");
			Console.WriteLine("path " + path); 
			property_names = new string[4];
			property_names [0] = "deadlock absence";
			property_names [1] = "no_irrelevant_barriers";
			property_names [2] = "no_object_leaks";
			property_names [3] = "no_comm_races";

			num_programs = number;


			//	my_certifier_tag= Communicator.world.Receive<int>(certifier,dataCertifierTactical);

			Console.WriteLine ("rank tactical "+ this.Rank + 
				" num progs:" + num_programs);
			programs = new string[num_programs];
			args_programs = new string[num_programs];

			num_units_program=new int[num_programs+1];
			programs_properties_status = new string[num_programs][];

		}

		void setUnitsProgs(ref int [] num_units_program){
			this.num_units_program = num_units_program;

			//Communicator.world.Receive<int>(certifier,dataCertifierTactical, ref num_units_program);
		}
		void setArgsProgs(ref string [] args_programs){
			this.args_programs = args_programs;

			//Communicator.world.Receive<string>(certifier,dataCertifierTactical, ref args_programs);
		}*/
		void setProgs(ref string [] programs){
			this.programs = programs;

			//Communicator.world.Receive<string>(certifier,dataCertifierTactical, ref programs);
			Console.WriteLine ("recebi tudo :" + this.Rank);
		}

		public void verify_(){

			if (num_threads > 1) {
				num_programs_thread = new int[num_threads];
				Thread[] threadv = new Thread[num_threads];
				int offset = (int)Math.Ceiling ((double)num_programs / num_threads);
				Console.WriteLine ("Rank ThreadOffset number_properties_unit " + this.Rank + " " + offset + " " + num_programs);
				int num_programs_aux = num_programs;
				for (int i = 0; i < num_threads; i++) {
					if (num_programs_aux > offset) {
						num_programs_thread [i] = offset;
						num_programs_aux -= offset;
					} else {
						num_programs_thread [i] = num_programs_aux;
						num_programs_aux = 0;
					}
					threadv [i] = new Thread ((ThreadStart)delegate() {
						int my_number_thread;
						int prog;
						lock (this) {
							my_number_thread = number_thread;
							number_thread++;
							prog = number_progs_consumed;
							number_progs_consumed += num_programs_thread [my_number_thread];
						}
						Console.WriteLine ("TACTICAL ParTypes - CREATING THREAD: " + my_number_thread + " from " + this.Rank + " num programs " + num_programs + " num programs thread " + num_programs_thread [my_number_thread]);
						for (int j = 0; j < num_programs_thread [my_number_thread]; j++) {

							Console.WriteLine ("Thread " + my_number_thread + " from " + 
								this.Rank + 
								" dealing with program " + programs[prog + j] );

							TacticalAdapterParTypes t = new TacticalAdapterParTypes (host, port, programs[prog + j]); 
								//num_units_program[prog + j], args_programs[prog + j], 9900+2*this.Rank + my_number_thread);
							int result = t.run ();
							Console.WriteLine ("Thread" + my_number_thread + " de " + this.Rank + ":Result of verification of program " +programs[prog + j]+ ": " + result + " storing status for " + (prog + j));
							if (result == -1){
								verification_is_inconclusive = true;
							}
							verification_status=result;
							//programs_properties_status[prog + j]=t.get_property_results();


						}

					});

					threadv [i].Start ();
				}

				for (int i = 0; i < num_threads; i++) {
					Console.WriteLine ("TACTICAL ParTypes - waiting threads : " + i + "from " + this.Rank);
					threadv [i].Join ();
				}
			} else {
				for (int j = 0; j < num_programs; j++) {

					Console.WriteLine ( this.Rank + " dealing with program " + programs[j]);
					TacticalAdapterParTypes t = new TacticalAdapterParTypes (host, port, programs[j]);
					int result = t.run ();
					Console.WriteLine ( this.Rank + ":Result of verification of program " +programs[j]+ ": " + result + " storing status for " + j);
					if (result == -1) {
						verification_is_inconclusive = true;
					}
					verification_status=result;
					//programs_properties_status[j]=t.get_property_results();

				}

			}




		}
	}

	interface VerificationInterface
	{
		int verify(string stringToPrint);
	}
	class TacticalAdapterParTypes

	{  
		string mpi_file; //int result;
		string host;
		VerificationInterface remoteObject;
		int port;
		//bool is_inconclusive = false;

		/*public static void _Main(){
			Console.WriteLine ("Teste Adaptador Tático ISP");

			TacticalAdapterISP t = new TacticalAdapterISP 
				("/home/allberson/Dropbox/HPC-Shelf-MapReduce/br.ufc.mdcc.hpcshelf.certifier.impl.computation.TacticalISPImpl/br.ufc.mdcc.hpcshelf.certifier.impl.computation.TacticalISPImpl/bin/Debug", 
					"mProjExecMPI", 4, "\"-$ /home/allberson/Dropbox/m101/rawdir /home/allberson/Dropbox/m101/images-rawdir.tbl /home/allberson/Dropbox/m101/template.hdr /home/allberson/Dropbox/m101/projdir /home/allberson/Dropbox/m101/stats.tbl\""
					,9000);
			int result = t.run ();

			Console.WriteLine ("Resultado da verificação das propriedades: " + result);
		}*/
		public TacticalAdapterParTypes (string host,  int port, string mpi_file)
		{
			this.host = host;
			this.mpi_file = mpi_file;
			/*this.number_units_mpi_file = number_units_mpi_file;
			this.params_mpi_file = params_mpi_file;*/
			this.port = port;
			/*property_results = new string[4];
			property_results[0]  =false;//deadlock_absence
			property_results[1]= true; //no_object_leak 
			property_results[2] = true; //no_comm_races 
			property_results[3]  = false;//no_irrelevant_barriers
			property_results [2] = "no MPI object leaks";
			property_results [3] = "no communication races"; */

		}
		/*public string[] get_property_results(){
			return property_results;
		}*/
		public int run(){

		
		
			int result;
			Type requiredType = typeof(VerificationInterface);

			remoteObject = (VerificationInterface)Activator.GetObject(requiredType,
			"tcp://"+host+":"+port+"/VerificationParTypes");
			result = remoteObject.verify(mpi_file);
			Console.WriteLine("Verification result of {0}: {1}",mpi_file,result);
			return result;//-1 inconclusiva  0-falso 1-verdadeira
			/*Process proc = new System.Diagnostics.Process ();
			proc.StartInfo.FileName = "/bin/bash";
			//proc.StartInfo.WorkingDirectory = "/home/00292431309/Dropbox/HPC-Shelf-MapReduce-master/br.ufc.mdcc.hpcshelf.mapreduce.impl.computation.ReducerImpl/src/1.0.0.0";

			proc.StartInfo.Arguments = path+"/run.sh " +number_units_mpi_file + " "+ mpi_file + " " + 
				params_mpi_file + " " +port;
			//Console.WriteLine ("Arguments " + proc.StartInfo.Arguments);
			proc.StartInfo.UseShellExecute = false; 
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.Start ();
			string output;

			while (!proc.StandardError.EndOfStream) {
				if(!is_inconclusive) is_inconclusive = true;
				output = proc.StandardError.ReadLine ();
				Console.WriteLine ("Log de Erro - Adaptador Tático ISP");
				//Console.WriteLine ("Arguments " + proc.StartInfo.Arguments);
				Console.WriteLine (output);
				//result Convert.ToBoolean(result);
			}

			//Console.WriteLine ("Saída padrão - Adaptador Tático ISP");
			while (!proc.StandardOutput.EndOfStream) {

				output = proc.StandardOutput.ReadLine ();
				//	Console.WriteLine (output);
				if (output == "ISP detected no deadlocks!") {
					//	Console.WriteLine (output);
					property_results[0] = "deadlock absence";

				}

				if (output == "There were no Irrelevant Barriers!") {
					//Console.WriteLine (output);
					property_results[1] = "no irrelevant barriers";

				}

				if (output.Contains("Resource leaks detected")) {
					//Console.WriteLine (output);
					property_results[2] = " ";

				}
				if (output.Contains("communication race")){
					//Console.WriteLine (output);
					property_results[3] = " ";

				}



				//Console.WriteLine (deadlock_absence.ToString() + no_irrelevant_barriers.ToString() + no_object_leak.ToString() + no_comm_races.ToString());

				//result Convert.ToBoolean(result);
			}

			if (deadlock_absence && no_irrelevant_barriers && no_object_leak && no_comm_races) {
				return 0;
			}
			if (is_inconclusive) {
				return -1;
			} else {
				return 0;
			}*/

		}
	}
}

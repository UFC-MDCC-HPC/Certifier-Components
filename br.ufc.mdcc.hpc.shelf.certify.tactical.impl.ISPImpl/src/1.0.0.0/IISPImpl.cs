using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ISP;
using System.Diagnostics;
namespace br.ufc.mdcc.hpc.shelf.certify.tactical.impl.ISPImpl
{
	public class IISPImpl : BaseIISPImpl, IISP
	{
		public string []programs; 
		public int num_programs; 
		public string[] args_programs;
		public int []num_units_program;
		public int num_threads = 1;
		public int number_thread=0; 
		public int number_progs_consumed=0;
		public bool verification_is_inconclusive = false;
		public int number_programs_consumed=0;
		public int [] num_programs_thread;
		public string [][] programs_properties_status;
		public string path;
		public string []property_names;
		public override void main()
		{



		}
	}
	class TacticalAdapterISP

	{  
		string mpi_file; int number_units_mpi_file; //int result;
		string path;
		string params_mpi_file;
		int port;
		bool is_inconclusive = false;
		string []property_results;

		public static void _Main(){
			Console.WriteLine ("Teste Adaptador Tático ISP\n");

			TacticalAdapterISP t = new TacticalAdapterISP 
				("/home/allberson/Dropbox/HPC-Shelf-MapReduce/br.ufc.mdcc.hpcshelf.certifier.impl.computation.TacticalISPImpl/br.ufc.mdcc.hpcshelf.certifier.impl.computation.TacticalISPImpl/bin/Debug", 
					"mProjExecMPI", 4, "\"-$ /home/allberson/Dropbox/m101/rawdir /home/allberson/Dropbox/m101/images-rawdir.tbl /home/allberson/Dropbox/m101/template.hdr /home/allberson/Dropbox/m101/projdir /home/allberson/Dropbox/m101/stats.tbl\""
					,9000);
			int result = t.run ();

			Console.WriteLine ("Resultado da verificação das propriedades: " + result);
		}
		public TacticalAdapterISP (string path, string mpi_file, int number_units_mpi_file, string params_mpi_file, int port)
		{
			this.path = path;
			this.mpi_file = mpi_file;
			this.number_units_mpi_file = number_units_mpi_file;
			this.params_mpi_file = params_mpi_file;
			this.port = port;
			property_results = new string[4];
			/*property_results[0]  =false;//deadlock_absence
			property_results[1]= true; //no_object_leak 
			property_results[2] = true; //no_comm_races 
			property_results[3]  = false;//no_irrelevant_barriers*/
			property_results [2] = "no MPI object leaks";
			property_results [3] = "no communication races"; 

		}
		public string[] get_property_results(){
			return property_results;
		}
		public int run(){

			Process proc = new System.Diagnostics.Process ();
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

			/*if (deadlock_absence && no_irrelevant_barriers && no_object_leak && no_comm_races) {
				return 0;
			}*/
			if (is_inconclusive) {
				return -1;
			} else {
				return 0;
			}

		}
	}
}

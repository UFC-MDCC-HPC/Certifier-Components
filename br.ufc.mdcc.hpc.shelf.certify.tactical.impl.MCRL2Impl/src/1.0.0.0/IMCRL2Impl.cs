using System;

using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.tactical.MCRL2;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;
using  br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using MPI;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;

namespace br.ufc.mdcc.hpc.shelf.certify.tactical.impl.MCRL2Impl
{
	public class IMCRL2Impl<S> : BaseIMCRL2Impl<S>, IMCRL2<S>
		where S: IVerifyDataPortServerTypeSWC2
	{   
		public string []property_files; 
		public int num_threads = 1;
		public int number_thread = 0;
		public string mCRL2_file; 
		public int num_properties;
		public int index_my_first_prop;
		public string path;
		public int number_units_tactical;
		public int[] properties_status;
		public bool verification_is_inconclusive = false;
		public int number_prop_consumed=0;
		public int [] num_properties_thread;
		public Boolean status_verification_properties;


	
		public override void main()
		{   
			while (true) 
			{
				Verify.invoke (IVerify.VERIFY_PERFORM);

				invoke_verify_perform ();

				if (!verification_is_inconclusive) 
					status_verification_properties = this.Communicator.Allreduce (false, Operation<Boolean>.LogicalAnd);
				else 
					status_verification_properties = this.Communicator.Allreduce (true, Operation<Boolean>.LogicalAnd);

				if (status_verification_properties)
					Verify.invoke (IVerify.VERIFY_CONCLUSIVE);
				else
					Verify.invoke (IVerify.VERIFY_INCONCLUSIVE);
			}
		}


		void setMcrl2File(ref string filename)
		{
			mCRL2_file = filename;
			path = System.Environment.GetEnvironmentVariable ("PATH_TAC_MCRL2_EXEC");
			Console.WriteLine ("path " + path);
			File.WriteAllText (@path + "/workflow.mcrl2", mCRL2_file);
		}

		void setNumProperties(int number)
		{
			num_properties = number;
		}

		void setIndexMyFirstProp(int index)
		{
			index_my_first_prop = index;

			number_units_tactical = this.Size - 1;

			Console.WriteLine ("rank tactical " + this.Rank + " num prop:" + num_properties);
			property_files = new string[num_properties];
		}

		void setPropertyFiles(ref string [] files)
		{			
			this.property_files = files;

			int index_property;
			for (int i = 0; i < num_properties; i++) 
			{
				index_property = index_my_first_prop + i;

				File.WriteAllText (@path + "/properties/property" + index_property + ".mcf", property_files [i]);

				Console.WriteLine (this.Rank + " " + "property" + index_property + ".mcf" + " --- " + property_files [i]);
			}

			properties_status = new int[num_properties];

			Console.WriteLine ("recebi tudo :" + this.Rank);
		}

		public void invoke_verify_perform()
		{
			if (num_threads > 1) 
			{
				num_properties_thread = new int[num_threads];
				Thread[] threadv = new Thread[num_threads];
				int offset = (int)Math.Ceiling ((double)num_properties / num_threads);
				Console.WriteLine ("Rank ThreadOffset number_properties_unit " + this.Rank + " " + offset + " " + num_properties);
				//assumo que cada nó de processamento possui 2 núcleos

				// "gnome-terminal -x bash -ic 'cd $HOME; ls; bash'";
				int num_properties_aux = num_properties;
				//string command = "run.sh" + mCRL2_file;
				for (int i = 0; i < num_threads; i++) 
				{
					if (num_properties_aux > offset) 
					{
						num_properties_thread [i] = offset;
						num_properties_aux -= offset;
					} else 
					{
						num_properties_thread [i] = num_properties_aux;
						num_properties_aux = 0;
					}

					threadv [i] = new Thread ((ThreadStart)delegate() 
					{
						int my_number_thread;
						int prop;

						lock (this) 
						{
							my_number_thread = number_thread;
							number_thread++;
							prop = number_prop_consumed;
							number_prop_consumed += num_properties_thread [my_number_thread];
						}

						int index_property;
						
						Console.WriteLine ("TACTICAL MCRL2 - CREATING THREAD: " + my_number_thread + " from " + this.Rank + " num properties " + num_properties + " num properties thread " + num_properties_thread [my_number_thread]);
						
						for (int j = 0; j < num_properties_thread [my_number_thread]; j++) 
						{
							index_property = index_my_first_prop + prop + j;

							Console.WriteLine ("Thread " + my_number_thread + " from " + this.Rank + " dealing with property " + "property" + index_property + ".mcf");

							TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 (path, "workflow.mcrl2", "property" + index_property + ".mcf");
							int result = t.run ();
							Console.WriteLine ("Thread" + my_number_thread + " de " + this.Rank + ":Result of verification of property " + "property" + index_property + ".mcf" + ": " + result + " storing status for " + (prop + j));
	
							properties_status [prop + j] = result;

							if (result == -1)
								verification_is_inconclusive = true;
						}
					});

					threadv [i].Start ();
				}

				for (int i = 0; i < num_threads; i++) 
				{
					Console.WriteLine ("TACTICAL MCRL2 - waiting threads : " + i + "from " + this.Rank);
					threadv [i].Join ();
				}
			} 
			else 
			{
				for (int j = 0; j < num_properties; j++) 
				{
					Console.WriteLine (this.Rank + " dealing with property " + "property" + (index_my_first_prop + j) + ".mcf");

					TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 (path, "workflow.mcrl2", "property" + (index_my_first_prop + j) + ".mcf");
					int result = t.run ();
					Console.WriteLine (this.Rank + ":Resultado da verificação da propriedade " + "property" + (index_my_first_prop + j) + ".mcf" + ": " + result + " storing status for " + j);

					properties_status [j] = result;

					if (result == -1)
						verification_is_inconclusive = true;
				}

			}

		}

		public void sendStatusVerification()
		{
			Console.WriteLine ("Rank " + this.Rank + " sending status verification to certifier ");
		}

	}

	class TacticalAdaptermCRL2
	{  
		string mCRL2_file; string property_file; //int result;
		string path;

/*		public static void _Main()
		{
			Console.WriteLine ("Teste Adaptador Tático mCRL2");

			TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 
				("~/Dropbox/HPC-Shelf-MapReduce/br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierSWC2Impl/br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierSWC2Impl/bin/Debug", "map-reduce.mcrl2", "property0.mcf");
			int result = t.run ();

			Console.WriteLine ("Resultado da verificação da propriedade: " + result);
		}
*/
		public TacticalAdaptermCRL2 (string path, string mCRL2_file, string property_file)
		{
			this.mCRL2_file = mCRL2_file;
			this.property_file = property_file;
			this.path = path;
		}

		public int run()
		{
			Process proc = new System.Diagnostics.Process ();
			proc.StartInfo.FileName = "/bin/bash";
			//proc.StartInfo.WorkingDirectory = "/home/00292431309/Dropbox/HPC-Shelf-MapReduce-master/br.ufc.mdcc.hpcshelf.mapreduce.impl.computation.ReducerImpl/src/1.0.0.0";

			proc.StartInfo.Arguments = path+"/run.sh " + path+"/"+ mCRL2_file + " " +  path+"/properties/"+ property_file;
			proc.StartInfo.UseShellExecute = false; 
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.Start ();
			string output;

			while (!proc.StandardOutput.EndOfStream) 
			{
				output = proc.StandardOutput.ReadLine ();
				Console.WriteLine ("Saída padrão - Adaptador Tático mCRL2 para prop " + property_file +" " + output);

				if (output == "true")      return 1;
				else if(output == "false") return 0;
			}

			return -1;
		}
	}
	
	
	
}

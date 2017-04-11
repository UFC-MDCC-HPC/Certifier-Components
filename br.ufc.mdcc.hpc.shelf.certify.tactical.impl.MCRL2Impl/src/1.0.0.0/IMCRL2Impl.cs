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
	public class IMCRL2Impl : BaseIMCRL2Impl, IMCRL2		
	{   
		public bool status_verification_properties;

		IVerifyDataPortServerTypeSWC2Impl service = null;
	
		public override void after_initialize ()
		{
			this.service = new IVerifyDataPortServerTypeSWC2Impl (this.Rank, this.Size);

			Verify_data.Server = service;
		}

		public override void main()
		{   
			while (true) 
			{
				Console.WriteLine ("STARTING ITERATION MCRL2 - BEFORE VERIFY PERFORM");

				Verify.invoke (IVerify.VERIFY_PERFORM);

				Console.WriteLine ("MCRL2 - AFTER VERIFY PERFORM");

				//Thread.Sleep (10000);
				service.invoke_verify_perform ();

				Console.WriteLine ("MCRL2 - invoke_verify_perform");

				if (service.Conclusive) 
					status_verification_properties = this.Communicator.Allreduce (false, Operation<Boolean>.LogicalAnd);
				else 
					status_verification_properties = this.Communicator.Allreduce (true, Operation<Boolean>.LogicalAnd);

				Console.WriteLine ("MCRL2 - After ALL REDUCE " + service.Conclusive);

				if (status_verification_properties)
					Verify.invoke (IVerify.VERIFY_CONCLUSIVE);
				else
					Verify.invoke (IVerify.VERIFY_INCONCLUSIVE);
				
				Console.WriteLine ("MCRL2 - END ITERATION " + status_verification_properties);
			}
		}

		internal class IVerifyDataPortServerTypeSWC2Impl : IVerifyDataPortServerTypeSWC2
		{
			public int num_threads = 1;
			public int [] num_properties_thread;
			public int[] properties_status;
			public int num_properties;
			public int number_thread = 0;
			public string []property_files; 
			public string mCRL2_file; 
			public int index_my_first_prop;
			public string work_path;
			public int number_units_tactical;
			public int number_prop_consumed=0;
			public bool verification_is_inconclusive = false;

			int Rank;
			int Size;

			public bool Conclusive { get { return verification_is_inconclusive; } }

			public IVerifyDataPortServerTypeSWC2Impl(int rank, int size)
			{
				this.Rank = rank;
				this.Size = size;
			}

			public void setMcrl2File(string filename)
			{
				Console.WriteLine (Rank + ": ENTER IVerifyDataPortServerTypeSWC2Impl - setMcrl2File");

				mCRL2_file = filename;
				work_path = System.Environment.GetEnvironmentVariable ("PATH_TAC_MCRL2_EXEC");
				if (work_path == null)
					work_path = System.Environment.GetEnvironmentVariable ("HOME");
				
				Console.WriteLine ("WORK PATH OF TACTICAL is " + work_path);

				File.WriteAllText (work_path + Path.DirectorySeparatorChar +"workflow.mcrl2", mCRL2_file);

				Console.WriteLine (Rank + ": EXIT IVerifyDataPortServerTypeSWC2Impl - setMcrl2File");
			}

			public void setNumProperties(int number)
			{
				Console.WriteLine (Rank + ": ENTER IVerifyDataPortServerTypeSWC2Impl - setNumProperties *** num_properties = {0}", num_properties);
				this.num_properties = number;
				Console.WriteLine (Rank + ": EXIT IVerifyDataPortServerTypeSWC2Impl - setNumProperties");
			}

			public void setIndexMyFirstProp(int index)
			{
				Console.WriteLine (Rank + ": ENTER IVerifyDataPortServerTypeSWC2Impl - setIndexMyFirstProp");

				index_my_first_prop = index;

				number_units_tactical = this.Size - 1;

				Console.WriteLine ("rank tactical " + this.Rank + " num prop:" + num_properties);
				property_files = new string[num_properties];

				Console.WriteLine (Rank + ": EXIT IVerifyDataPortServerTypeSWC2Impl - setIndexMyFirstProp");
			}

			public void setPropertyFiles(string [] files)
			{			
				Console.WriteLine (Rank + ": ENTER IVerifyDataPortServerTypeSWC2Impl - setPropertyFiles");
				property_files = files;

				int index_property;
				for (int i = 0; i < num_properties; i++) 
				{
					index_property = index_my_first_prop + i;

					File.WriteAllText (work_path + Path.DirectorySeparatorChar + "property" + index_property + ".mcf", property_files [i]);

					Console.WriteLine (Rank + " " + "property" + index_property + ".mcf" + " --- " + property_files [i]);
				}

				properties_status = new int[num_properties];

				Console.WriteLine (Rank + ": EXIT IVerifyDataPortServerTypeSWC2Impl - setPropertyFiles");
			}

			public void invoke_verify_perform()
			{
				Console.WriteLine (Rank + ": ENTER IVerifyDataPortServerTypeSWC2Impl - invoke_verify_perform");
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

									TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 (work_path, "workflow.mcrl2", "property" + index_property + ".mcf");
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
						Console.WriteLine ("TACTICAL MCRL2 - waiting threads : " + i + " from " + this.Rank);
						threadv [i].Join ();
					}
				} 
				else 
				{
					Console.WriteLine (this.Rank + ": num_properties = {0}", num_properties);

					for (int j = 0; j < num_properties; j++) 
					{
						Console.WriteLine (this.Rank + " dealing with property " + "property" + (index_my_first_prop + j) + ".mcf");

						TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 (work_path, "workflow.mcrl2", "property" + (index_my_first_prop + j) + ".mcf");
						int result = t.run ();
						Console.WriteLine (this.Rank + ":Resultado da verificação da propriedade " + "property" + (index_my_first_prop + j) + ".mcf" + ": " + result + " storing status for " + j);

						properties_status [j] = result;

						if (result == -1)
							verification_is_inconclusive = true;
					}

				}

				Console.WriteLine (Rank + ": EXIT IVerifyDataPortServerTypeSWC2Impl - invoke_verify_perform");
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
		string work_path;

		public TacticalAdaptermCRL2 (string work_path, string mCRL2_file, string property_file)
		{
			this.mCRL2_file = mCRL2_file;
			this.property_file = property_file;
			this.work_path = work_path;
		}

		public int run()
		{
			Console.WriteLine ("BEGIN TACTICAL ADAPTER");

			Process proc = new System.Diagnostics.Process ();
			proc.StartInfo.FileName = "/bin/bash";
			//proc.StartInfo.WorkingDirectory = "/home/00292431309/Dropbox/HPC-Shelf-MapReduce-master/br.ufc.mdcc.hpcshelf.mapreduce.impl.computation.ReducerImpl/src/1.0.0.0";

			proc.StartInfo.Arguments = work_path + Path.DirectorySeparatorChar + "run.sh " + work_path + Path.DirectorySeparatorChar + mCRL2_file + " " + work_path + Path.DirectorySeparatorChar + property_file;
			proc.StartInfo.UseShellExecute = false; 
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			Console.WriteLine ("TACTICAL ADAPTER - BEFORE START");
			proc.Start ();
			Console.WriteLine ("TACTICAL ADAPTER - AFTER START");
			string output;
				
			while (!proc.StandardOutput.EndOfStream) 
			{
				output = proc.StandardOutput.ReadLine ();
				Console.WriteLine ("Saída padrão - Adaptador Tático mCRL2 para prop " + property_file +" " + output);

				if (output == "true")      return 1;
				else if(output == "false") return 0;
			}

			Console.WriteLine ("END TACTICAL ADAPTER");
			return -1;
		}
	}
	
	
	
}

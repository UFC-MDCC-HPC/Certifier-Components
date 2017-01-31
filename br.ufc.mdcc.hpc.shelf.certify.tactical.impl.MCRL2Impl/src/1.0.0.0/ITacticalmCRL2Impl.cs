using System;
using MPI;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;

namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.TacticalImpl
{
	class ITacticalmCRL2Impl : ITactical


		//IMPORTANTE: SUPONHO QUE O CERTIFICADOR TEM RANK 0, O NÓ MASTER DO TÁTICO RANK 1 E OS DEMAIS NÓS DO TÁTICO TEM OS RANKS EM SEGUIDA
	{   string mCRL2_file; 

		//int number_properties_per_unit_tactical=0;
		static void Main(string[] args)
		{

				/*Console.Write("Rank {0} of {1} running on {2}\n",
				              Communicator.world.Rank,
				              Communicator.world.Size,
				              MPI.Environment.ProcessorName);*/
			MPI.Environment e =  new MPI.Environment(ref args, Threading.Multiple);
			ITacticalmCRL2Impl t = new ITacticalmCRL2Impl ();
			t.receiveDataFromCertifier();
			t.invoke_verify_perform ();
			if (!t.verification_is_inconclusive) {
				t.invoke_verify_conclusive ();
			} else {
				t.invoke_verify_inconclusive ();
			}
		    t.sendStatusVerification ();
			e.Dispose ();
		}

		public void receiveDataFromCertifier(){
			//Console.WriteLine (Communicator.world.Rank);
			Communicator.world.Broadcast<string>(ref mCRL2_file,certifier);
			//Communicator.world.Broadcast<int>(ref num_properties_total,certifier);
			//mCRL2_file = Communicator.world.Receive<string>(certifier,dataCertifierTactical);
			//Console.WriteLine (mCRL2_file);
			//path = System.Environment.ExpandEnvironmentVariables("");

			path =System.Environment.GetEnvironmentVariable("PATH_TAC_MCRL2_EXEC");
			Console.WriteLine("path " + path); 
			File.WriteAllText(@path+"/workflow.mcrl2", mCRL2_file);
			//Communicator.world.Receive<int>(certifier,dataCertifierTactical, ref num_properties);

			//Communicator.world.Broadcast<int>(ref num_properties,certifier);
			num_properties = Communicator.world.Receive<int>(certifier,dataCertifierTactical);
			index_my_first_prop = Communicator.world.Receive<int>(certifier,dataCertifierTactical);
			//Console.WriteLine (num_properties);

			number_units_tactical = Communicator.world.Size - 1;
			//number_properties_per_unit_tactical = num_properties / number_units_tactical;
			Console.WriteLine ("rank tactical "+ Communicator.world.Rank + " num prop:" + num_properties);
			property_files = new string[num_properties];
			//Communicator.world.Receive<string>(certifier,dataCertifierTactical, ref property_files);
			//Communicator.world.ScatterFromFlattened<string>(property_files, num_properties, certifier);

			Communicator.world.Receive<string>(certifier,dataCertifierTactical, ref property_files);
			int index_property;
			for (int i=0; i<num_properties; i++) {
				index_property =  index_my_first_prop + i;

				File.WriteAllText(@path+"/properties/property"+index_property+".mcf", property_files[i]);
				Console.WriteLine (Communicator.world.Rank + " " + "property"+index_property+".mcf" + "\n"+property_files[i]);
			}
			properties_status = new int[num_properties];
			//properties_status2 = new ConcurrentDictionary<string,int>();
			Console.WriteLine ("recebi tudo :" + Communicator.world.Rank);
		}
		public void invoke_verify_perform(){

			Communicator.world.Receive<int>(certifier,verify_perform);
			verify ();

		}

		public void verify(){
			if (num_threads > 1) {
				num_properties_thread = new int[num_threads];
				Thread[] threadv = new Thread[num_threads];
				int offset = (int)Math.Ceiling ((double)num_properties / num_threads);
				Console.WriteLine ("Rank ThreadOffset number_properties_unit " + Communicator.world.Rank + " " + offset + " " + num_properties);
				//assumo que cada nó de processamento possui 2 núcleos

				// "gnome-terminal -x bash -ic 'cd $HOME; ls; bash'";
				int num_properties_aux = num_properties;
				//string command = "run.sh" + mCRL2_file;
				for (int i = 0; i < num_threads; i++) {
					if (num_properties_aux > offset) {
						num_properties_thread [i] = offset;
						num_properties_aux -= offset;
					} else {
						num_properties_thread [i] = num_properties_aux;
						num_properties_aux = 0;

					}
					threadv [i] = new Thread ((ThreadStart)delegate() {
						int my_number_thread;
						int prop;
						lock (this) {
							my_number_thread = number_thread;
							number_thread++;
							prop = number_prop_consumed;
							number_prop_consumed += num_properties_thread [my_number_thread];
						}
						int index_property;
						Console.WriteLine ("TACTICAL MCRL2 - CREATING THREAD: " + my_number_thread + " from " + Communicator.world.Rank + " num properties " + num_properties + " num properties thread " + num_properties_thread [my_number_thread]);
						for (int j = 0; j < num_properties_thread [my_number_thread]; j++) {

							index_property = index_my_first_prop + prop + j;
								

							Console.WriteLine ("Thread " + my_number_thread + " from " + Communicator.world.Rank + " dealing with property " + "property" + index_property + ".mcf");
			                         
							TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 (path, "workflow.mcrl2", "property" + index_property + ".mcf");
							int result = t.run ();
							Console.WriteLine ("Thread" + my_number_thread + " de " + Communicator.world.Rank + ":Result of verification of property " + "property" + index_property + ".mcf" + ": " + result + " storing status for " + (prop + j));
							//	Console.WriteLine("Storing status of index );
							properties_status [prop + j] = result;
							//lock(this){
							//properties_status[index_property] = result;
							//properties_status2.TryAdd(index_property+".mcf", result);
							//}
							if (result == -1)
								verification_is_inconclusive = true;



						}

					});

					threadv [i].Start ();
				}

				for (int i = 0; i < num_threads; i++) {
					Console.WriteLine ("TACTICAL MCRL2 - waiting threads : " + i + "from " + Communicator.world.Rank);
					threadv [i].Join ();
				}
			} else {
				for (int j = 0; j < num_properties; j++) {




					Console.WriteLine ( Communicator.world.Rank + " dealing with property " + "property" + (index_my_first_prop+j) + ".mcf");

					TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 (path, "workflow.mcrl2", "property" + (index_my_first_prop+j) + ".mcf");
					int result = t.run ();
					Console.WriteLine (Communicator.world.Rank + ":Resultado da verificação da propriedade " + "property" + (index_my_first_prop+j) + ".mcf" + ": " + result + " storing status for " + j);
					//	Console.WriteLine("Storing status of index );
					properties_status [j] = result;
					//lock(this){
					//properties_status[index_property] = result;
					//properties_status2.TryAdd(index_property+".mcf", result);
					//}
					if (result == -1)
						verification_is_inconclusive = true;



				}
			
			}

		}
		public void invoke_verify_conclusive(){
			//Console.WriteLine ("invoking verify_conclusive");
			Communicator.world.Send<int>(Communicator.world.Rank + 100,certifier,status_verification);

		}
		public void sendStatusVerification(){
			Console.WriteLine ("Rank "+ Communicator.world.Rank + " sending status verification to certifier ");
			//Communicator.world.Send<ConcurrentDictionary<string,int>>(verify_conclusive,certifier,status_verification);

			//for(int j = 0 ; j < num_properties;j++){
			//	Console.WriteLine ("Status verification of property " + j + " : " +properties_status[j]);
				//properties_status [j] = -2;

			//}
			//
			//br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.Properties prop = new Properties(99);
			//prop.properties_status2 = properties_status2;
			Communicator.world.Send<int>(properties_status,certifier, status_verification_properties);
			//if (Communicator.world.Rank == 1) {
			//	Communicator.world.Send<Properties> (prop, certifier, status_verification_properties);
			//}

		}
		public void invoke_verify_inconclusive(){

			//Console.WriteLine ("invoking verify_inconclusive");
			Communicator.world.Send<int>(Communicator.world.Rank+200,certifier,status_verification);
		}
	}
}
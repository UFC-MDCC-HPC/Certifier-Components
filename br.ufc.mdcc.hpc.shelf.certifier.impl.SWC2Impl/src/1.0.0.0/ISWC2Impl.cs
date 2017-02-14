using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certifier.SWC2;
using br.ufc.mdcc.hpc.shelf.certifier.Certifier;
using System.Diagnostics;
using System.IO;
using  br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;


namespace br.ufc.mdcc.hpc.shelf.certifier.impl.SWC2Impl
{
	public class ISWC2Impl : BaseISWC2Impl, ICertifier
	{

		public string mCRL2_file="map-reduce-test.mcrl2";
		public int num_properties=20;
		public int certifier = 0;
		public string []properties_files;
		public int number_units_tactical;
		public int [] number_properties_per_unit_tactical;
		public int slice_size;
		public int[] properties_status;
		public int []index_first_prop_tact;
		public override void main()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			instantiateVectors();
			sendDataToTactical ();
			Verify1.invoke (IVerify.VERIFY_PERFORM);																																																																																																																																																																																																																																																																										


			IActionFutureSet future_iteration = null;

			IActionFuture future_conclusive = null; 
			Verify1.invoke (IVerify.VERIFY_CONCLUSIVE, out future_conclusive);

			future_iteration = future_conclusive.createSet ();

			IActionFuture future_inconclusive = null; 
			Verify1.invoke (IVerify.VERIFY_INCONCLUSIVE, out future_inconclusive);
			future_iteration.addAction (future_inconclusive);

			IActionFuture action = future_iteration.waitAny ();

			sw.Stop();

			Console.WriteLine("Tempo total de verificação={0}",sw.Elapsed);
		}


		public void instantiateVectors(){
			properties_files = new string[num_properties];
			number_units_tactical = this.Verify_data1.Client.getRemoteSize ();
				//Communicator.world.Size - 1;

			number_properties_per_unit_tactical = new int[number_units_tactical];
			slice_size = (int) Math.Floor( (double)num_properties / number_units_tactical);
			int num_properties_aux = num_properties;
			for(int i=0;i<number_units_tactical;i++){
				number_properties_per_unit_tactical [i] = slice_size;
				num_properties_aux = num_properties_aux - slice_size;

		
			}
			int index=0;
			for(int i =0 ; i< num_properties_aux;i++){
				number_properties_per_unit_tactical [index++] += 1;
				if (index == number_units_tactical){
					index = 0;

				}
			}
			num_properties_aux = 0;



		}
		public void sendDataToTactical(){
			StreamReader sr = new StreamReader(mCRL2_file);
			string line = sr.ReadToEnd();
			Verify_data1.Client.setMcrl2File (ref line);
			//Communicator.world.Broadcast<string>(ref line,certifier);
			int i;
			for (i=0; i<num_properties; i++) {
				sr = new StreamReader("property"+i+".mcf");
				line = sr.ReadToEnd();
				properties_files [i] = line;
			}
			string[] arr = new string[slice_size+1];

			properties_status = new int[slice_size+1];
			index_first_prop_tact = new int[number_units_tactical];
			int number_prop_read=0;
			for(i=0;i<number_units_tactical;i++){
				
				Verify_data1.Client.setNumProperties (i, number_properties_per_unit_tactical [i]);
				//Communicator.world.Send<int>(number_properties_per_unit_tactical[i],tacticalFirstUnit+i,dataCertifierTactical);
				Verify_data1.Client.setIndexMyFirstProp (i, number_prop_read);

				//Communicator.world.Send<int>(number_prop_read,tacticalFirstUnit+i,dataCertifierTactical);
				index_first_prop_tact [i] = number_prop_read;

				//Console.WriteLine ("i unit numberunittact numbertactperunit "+i + " "+  (tacticalFirstUnit+i) + " " + number_units_tactical + " "+  number_properties_per_unit_tactical[i] + " "+ " "+ " "+ " "+ " "+ " "+ " ");
				Array.Copy (properties_files, number_prop_read, arr, 0, number_properties_per_unit_tactical[i]);
				number_prop_read += number_properties_per_unit_tactical[i];

				for(int j = 0; j<number_properties_per_unit_tactical[i];j++){

					Console.WriteLine (arr[j]);
				}
				Console.WriteLine ("proximo");
				Verify_data1.Client.setPropertyFiles (i,ref arr);

				//Communicator.world.Send<string>(arr,tacticalFirstUnit+i,dataCertifierTactical);
				Console.WriteLine ("proximo2");
			}

		}
	}
}

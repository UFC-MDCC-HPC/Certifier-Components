using System;
using MPI;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using System.Xml.Schema;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;

using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;

namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl
{
	class ICertifierImplC4_MPI : ICertifier

{  

		static void Main(string[] args)
		{

			
			Stopwatch sw = new Stopwatch();
			sw.Start();

			MPI.Environment e = 
				new MPI.Environment(ref args, Threading.Multiple);

			ICertifierImplC4_MPI c = new ICertifierImplC4_MPI ();
              
			/*	Console.Write("Rank {0} of {1} running on {2}\n",
				              Communicator.world.Rank,
				              Communicator.world.Size,
				              MPI.Environment.ProcessorName);
			}*/
			//CertifierConsoleLogger.write("Return of Certification Process: " + ret);
			c.sendDataToTactical();

			c.performOrchestration ();

			c.finishCertifier ();


			/*c.invoke_verify_perform ();
			int ret = c.wait_verify_tactical ();
			if ( ret== c.verify_inconclusive) {
			
				Console.WriteLine ("Some verification of some program was inconclusive");
			}else if(ret == c.verify_conclusive){

				Console.WriteLine ("The verification of all programs was conclusive");
			}*/

			e.Dispose ();
			sw.Stop();




			Console.WriteLine("Total time of verification={0}",sw.Elapsed);

		}
		public ICertifierImplC4_MPI(){
			Console.WriteLine ("number of procs " + Communicator.world.Size);

			/*tactical_responsible_prog=new int[number_tacticals];

			tactical_responsible_prog [0] = tactical_responsible_prog [1] = 4;
				tactical_responsible_prog [2] = tactical_responsible_prog [3] = "ISP1";
					tactical_responsible_prog [4] = tactical_responsible_prog [5] = 
						tactical_responsible_prog [6] = tactical_responsible_prog [7] ="ISP2";
*/

			//programs_verification_status = new int[num_programs];

			fillInputData ();


			//number_progs_per_unit_tactical = new int[number_units_tactical];
			number_progs_per_unit_tactical = new int[number_tacticals][];
			result_verification_unit_tactical = new int[number_tacticals][];

			slice_size_tacticals=new int[number_tacticals];

			tactical_first_unit = new int[number_tacticals];
			status_verification=new int[number_tacticals];


			int num_progs_aux; int index; int index_first_unit=1;

			for(int i=0;i<number_tacticals;i++){
				number_progs_per_unit_tactical[i]=new int[number_units_tacticals[i]];
				result_verification_unit_tactical[i]=new int[number_units_tacticals[i]];
				slice_size_tacticals[i] = (int) Math.Floor( (double)number_progs_per_tactical[i] / number_units_tacticals[i]);
				tactical_first_unit [i] = index_first_unit;
				status_verification [i] = i+400;
				index_first_unit += number_units_tacticals [i];
				 num_progs_aux = number_progs_per_tactical[i];
				for (int j = 0; j < number_units_tacticals [i]; j++) {
					number_progs_per_unit_tactical[i][j] = slice_size_tacticals[i];
					num_progs_aux = num_progs_aux - slice_size_tacticals[i];
				}
				index=0;
				for(int j=0 ; j< num_progs_aux;j++){
					number_progs_per_unit_tactical[i][index++]+= 1;
					if (index == number_units_tacticals[i]){
						index = 0;

					}

				}
				num_progs_aux = 0;



			}

			for(int i=0;i<number_tacticals;i++){
				Console.WriteLine (" i " + i+ " TACTICAL " + tactical_names [i] + " slice_size_tacticals " + slice_size_tacticals [i] +
					" tactical_first_unit " + tactical_first_unit[i]);
				for (int j = 0; j < number_units_tacticals [i]; j++) {
					Console.WriteLine (" j " + j + " number_progs_per_unit_tactical " + number_progs_per_unit_tactical[i][j]);
					
					
					
				
				}

			}


		}
		public void finishCertifier(){
			Console.WriteLine ("iterating remaining start threads");
			foreach (KeyValuePair <String, LogicActionInstantiate> entry in InstantiateActions)
			{
				if (entry.Value._thread != null) {
					entry.Value._thread.Join ();
				}
			}
			foreach (KeyValuePair <String, LogicActionCompute> entry in ComputeActions)
			{
				if (entry.Value._thread != null) {
				entry.Value._thread.Join();
				}

			}
		}
	/*	public void invoke_verify_perform(){
			//Console.WriteLine ("invoking verify_perform");
			for (int i = 0; i < number_tacticals; i++) {
				for (int j = 0; j < number_units_tacticals [i]; j++) {
					Communicator.world.Send<int> (0, tactical_first_unit[i]+j, verify_perform);
				}
			}
		}*/
		public void performOrchestration(){

			CertifierOrchestrationParser parser = new CertifierOrchestrationParser();
			parser.readOrchestrationXML(CertifierConstants.ORCHESTRATION_FILE_TEST);
			CertifierConsoleLogger.write(parser.getOrchestration().toString()); // get orchestration from the concrete certifier component

			variables.Add ("formal_properties", formal_properties);

			//int ret=
				parser.getOrchestration().run(); // run orchestration to prove formal contracts


			//Console.WriteLine("Status of orchestration "+ ret);



			/*

			//using (FileStream xmlStream = new FileStream("./xml/orchestration_swc2.xml", FileMode.Open))
			using (FileStream xmlStream = new FileStream("./xml/orchestration_sequence_parallel_start.xml", FileMode.Open))

			{
				using (XmlReader xmlReader = XmlReader.Create(xmlStream))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(XMLCertifierOperation));
					//XMLCertifierOperation op = serializer.Deserialize(xmlReader) as XMLCertifierOperation ;
					XMLCertifierOperation op = serializer.Deserialize(xmlReader) as XMLCertifierOperation ;

					Console.WriteLine("op.Item.oper_name " + op.Item.oper_name);
					Console.WriteLine("op.ItemElementName " + op.ItemElementName);
					Console.WriteLine("op.oper name " + op.oper_name);
				




				}




			}*/
		}
		public void fillInputData(){
			tactical_names = new string[number_tacticals];
			tactical_names [0] = "ISP1"; tactical_names [1] = "ISP2";

			number_units_tacticals=new int[number_tacticals];
			number_units_tacticals [0] = number_units_tacticals [1] = 2;

			number_progs_per_tactical = new int[number_tacticals];
			number_progs_per_tactical [0] = 4;
			number_progs_per_tactical [1] = 3;


			programs = new string[num_programs][];

			programs [0] = new string[number_progs_per_tactical [0]];
			programs [1] = new string[number_progs_per_tactical [1]];


			programs [0] [0] = "mProjExecMPI1";
				programs [0][1] = "mProjExecMPI2";
			programs [0][2] ="mProjExecMPI3";
				programs [0][3] = "mProjExecMPI4";
			programs [1][0] = "mProjExecMPI5";
				programs [1][1] ="mProjExecMPI6";
			programs [1] [2] = "mProjExecMPI7";
				//programs [1][3] 				= "mProjExecMPI8";

			num_units_program=new int[num_programs][];

			num_units_program [0] = new int[number_progs_per_tactical [0]];
			num_units_program [1] = new int[number_progs_per_tactical [1]];

			arr_transfer_prop = new string[number_tacticals][];
			arr_transfer_prop[0] = new string[4];
			arr_transfer_prop[1] = new string[4];

			num_units_program [0] [0] = 4;
				num_units_program [0][1] = 4;
			num_units_program [0] [2] = 4;
			num_units_program [0][3] = 4;
			num_units_program [1] [0] = 4;
			num_units_program [1][1] = 4;
			num_units_program [1] [2] = 4;
				//num_units_program [1][3] 				= "8";


			args_programs = new string[num_programs][];
			args_programs[0]=new string[number_progs_per_tactical [0]];
			args_programs[1]=new string[number_progs_per_tactical [1]];
			args_programs[0][0] = 	 "\"-$ /home/allberson/Dropbox/m101/rawdir /home/allberson/Dropbox/m101/images-rawdir.tbl /home/allberson/Dropbox/m101/template.hdr /home/allberson/Dropbox/m101/projdir /home/allberson/Dropbox/m101/stats.tbl\"";

			args_programs[0][1] =  "\"-$ /home/allberson/Dropbox/m102/rawdir /home/allberson/Dropbox/m102/images-rawdir.tbl /home/allberson/Dropbox/m102/template.hdr /home/allberson/Dropbox/m102/projdir /home/allberson/Dropbox/m102/stats.tbl\"";

			args_programs[0][2] =  "\"-$ /home/allberson/Dropbox/m103/rawdir /home/allberson/Dropbox/m103/images-rawdir.tbl /home/allberson/Dropbox/m103/template.hdr /home/allberson/Dropbox/m103/projdir /home/allberson/Dropbox/m103/stats.tbl\"";

			args_programs[0][3] = "\"-$ /home/allberson/Dropbox/m104/rawdir /home/allberson/Dropbox/m104/images-rawdir.tbl /home/allberson/Dropbox/m104/template.hdr /home/allberson/Dropbox/m104/projdir /home/allberson/Dropbox/m104/stats.tbl\"";

			args_programs[1][0] = 	 "\"-$ /home/allberson/Dropbox/m105/rawdir /home/allberson/Dropbox/m105/images-rawdir.tbl /home/allberson/Dropbox/m105/template.hdr /home/allberson/Dropbox/m105/projdir /home/allberson/Dropbox/m105/stats.tbl\"";

			args_programs[1][1] = "\"-$ /home/allberson/Dropbox/m106/rawdir /home/allberson/Dropbox/m106/images-rawdir.tbl /home/allberson/Dropbox/m106/template.hdr /home/allberson/Dropbox/m106/projdir /home/allberson/Dropbox/m106/stats.tbl\"";

			args_programs[1][2] =  "\"-$ /home/allberson/Dropbox/m107/rawdir /home/allberson/Dropbox/m107/images-rawdir.tbl /home/allberson/Dropbox/m107/template.hdr /home/allberson/Dropbox/m107/projdir /home/allberson/Dropbox/m107/stats.tbl\"";

			//args_programs[1][3] = "\"-$ /home/allberson/Dropbox/m108/rawdir /home/allberson/Dropbox/m108/images-rawdir.tbl /home/allberson/Dropbox/m108/template.hdr /home/allberson/Dropbox/m108/projdir /home/allberson/Dropbox/m108/stats.tbl\"";
			//= "\"-$ /home/allberson/Dropbox/m101/rawdir /home/allberson/Dropbox/m101/images-rawdir.tbl /home/allberson/Dropbox/m101/template.hdr /home/allberson/Dropbox/m101/projdir /home/allberson/Dropbox/m101/stats.tbl\"";
		//	formal_properties.Add("mProjExecMPI1","no deadlock",true);

		}
		public void sendDataToTactical(){
			//StreamReader sr = new StreamReader(mCRL2_file);
			//string line = sr.ReadToEnd();
			//Console.WriteLine(line);
			int number_prog_read;

			index_first_prog_unit_tactical = new int[number_tacticals][];
			arr_transfer_tac = new string[number_tacticals][];
			arr_int_transfer_tac = new int[number_tacticals][];

			//Console.WriteLine (Communicator.world.Rank);
			//Communicator.world.Broadcast<string>(ref programs,certifier);
			for (int i = 0; i < number_tacticals; i++) {
				
				
			
			
				//Communicator.world.Broadcast<int>(ref num_properties,certifier);
				//Communicator.world.Send<int>(num_properties,masterTactical,dataCertifierTactical);

				/*for(int i=0;i<num_properties;i++){
				Communicator.world.Scatter<string>(properties_files);

			}*/

				//Console.WriteLine (number_properties_per_unit_tactical);
				//Communicator.world.ScatterFromFlattened<string>(properties_files, number_properties_per_unit_tactical, certifier);
				//ArraySegment <string> a;
				arr_transfer_tac[i] = new string[slice_size_tacticals[i] + 1];
				arr_int_transfer_tac[i] = new int[slice_size_tacticals[i] + 1];
				index_first_prog_unit_tactical[i]=new int[number_units_tacticals[i]];
				//properties_status = new int[slice_size_tacticals[i] + 1];
				//programs_verification_status = new int[slice_size_tacticals[i] + 1];
				//index_first_prog_unit_tactical[i] = new int[number_units_tacticals[i]];
				//	properties_status2 = new ConcurrentDictionary<string,int>();
				number_prog_read = 0;
				for (int j = 0; j < number_units_tacticals[i]; j++) {



					
					//Communicator.world.Broadcast<int>(ref number_properties_per_unit_tactical,certifier);
					Communicator.world.Send<int> (number_progs_per_unit_tactical [i][j], tactical_first_unit[i]+j , dataCertifierTactical);
					Communicator.world.Send<int> (status_verification[i], tactical_first_unit[i]+j , dataCertifierTactical);
					//Communicator.world.Send<int> (number_prog_read, tactical_first_unit[i]+j, dataCertifierTactical);
					index_first_prog_unit_tactical [i][j] = number_prog_read;
					Console.WriteLine (" i " + i + " j " + j + " tactical_first_unit[i]+j " + (tactical_first_unit [i] + j)
						+ " number_progs_per_unit_tactical [i] " + number_progs_per_unit_tactical [i][j] +
						" index_first_prog_unit_tactical [i][j] " + index_first_prog_unit_tactical [i] [j]
						+" number_prog_read " +number_prog_read);

					//Console.WriteLine ("proximo-3");
					//Console.WriteLine ("i j unit numberunittact numbertactperunit " + i + " " + j + " " + (tactical_first_unit[i] + j) + " " 
					//	+ number_units_tacticals[i] + " " + number_progs_per_unit_tactical [i][j] );
					//a = new ArraySegment<string> (properties_files, i * number_units_tactical, number_properties_per_unit_tactical);

					Console.WriteLine ("proximo2");

					Array.Copy (num_units_program[i], number_prog_read, arr_int_transfer_tac[i], 0, number_progs_per_unit_tactical [i][j]);
					for (int k = 0; k < number_progs_per_unit_tactical [i][j]; k++) {
						Console.WriteLine ("unities of programs to " + (tactical_first_unit[i]+j));
						Console.WriteLine (arr_int_transfer_tac[i] [k]);
					}
					Communicator.world.Send<int> (arr_int_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);


					Array.Copy (args_programs[i], number_prog_read, arr_transfer_tac[i], 0, number_progs_per_unit_tactical [i][j]);
					for (int k = 0; k < number_progs_per_unit_tactical [i][j]; k++) {
						Console.WriteLine ("args");
						Console.WriteLine (arr_transfer_tac[i] [k]);
					}
					Communicator.world.Send<string> (arr_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);


					Array.Copy (programs[i], number_prog_read, arr_transfer_tac[i], 0, number_progs_per_unit_tactical [i][j]);

					//string[] sub = properties_files.SubArray<string>(3, 4);

					for (int k = 0; k < number_progs_per_unit_tactical [i][j]; k++) {
						Console.WriteLine ("progs");
						Console.WriteLine (arr_transfer_tac[i][k]);
					}
					Console.WriteLine ("proximo");

					Communicator.world.Send<string> (arr_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);

				


					number_prog_read += number_progs_per_unit_tactical [i][j];
				}
			}

		}
	

		/*

		public int wait_verify_tactical(){
			//Console.WriteLine ("waiting verify from tactical");
			//CompletedStatus s;
			int ret;
			int retorno = verify_conclusive;
			//int offset=0;
			//Console.WriteLine ("proxim3");
			for (int i = 0; i < number_tacticals; i++) {
				for (int j=0; j<number_units_tacticals[i]; j++) {
					ret = Communicator.world.Receive<int> (Communicator.anySource, status_verification[i]);
						//Communicator.world.Receive<ConcurrentDictionary<string,int>>(Communicator.anySource,status_verification_properties, out properties_status2);
						//properties_status2 = Communicator.world.Receive<ConcurrentDictionary<string,int>> (Communicator.anySource, status_verification);
						if (ret > 200) {//inconclusive
							retorno = verify_inconclusive;
							ret = ret - 200;
						} else {
							ret = ret - 100;
						}


						//int[] hostnames = Communicator.world.Gather(, 0);
						Console.WriteLine ("certifier received verification from "+ret);
				



						//properties_status = Communicator.world.Receive<Dictionary<string,int>>(Communicator.anySource, Communicator.anyTag);
						//Communicator.world.Receive(s.Source,status_verification, ref properties_status);
						//status_verification = Communicator.world.Receive<Dictionary<string,int>>(certifier,status_verification);
						//Communicator.world.Receive<Dictionary<string,int>> (Communicator.anySource, Communicator.anyTag, out ret, out s);
						//Communicator.world.Receive<Dictionary<string,int>>(s.Source,status_verification, out properties_status, out s1);
					}
			}

			return retorno;

			//if(ret != verify_inconclusive) return verify_conclusive;
		}*/

	}
}

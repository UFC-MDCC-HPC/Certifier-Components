using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certifier.C4;
using br.ufc.mdcc.hpc.shelf.certifier.Certifier;
using System.Diagnostics;
using br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortImpl;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using System.Threading;
using System.Xml.Schema;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ExpressionEvaluator;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;

namespace br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl
{
	public class C4Impl : BaseC4Impl, ICertifier
	{
		

		public static ConcurrentDictionary <string, LogicActionInstantiate> InstantiateActions = new ConcurrentDictionary<string, LogicActionInstantiate>();
		public static ConcurrentDictionary <string, LogicActionCompute> ComputeActions = new ConcurrentDictionary<string, LogicActionCompute>();
		public static TypeRegistry variables = new TypeRegistry();
		public static MultiKeyConcurrentDictionary <string,string, bool> formal_properties = new MultiKeyConcurrentDictionary <string, string, bool>();
		public int num_programs=7;
		public static string[][] arr_transfer_prop;
		public static string[][] arr_transfer_tac;
		public static int[][] arr_int_transfer_tac;
		public int []slice_size_tacticals;
		public static int [][]index_first_prog_unit_tactical;
		public static string [][]programs; 
		public static int number_tacticals=2;
		public static string[] tactical_names;
		public static int []number_units_tacticals;
		public static int [][] number_progs_per_unit_tactical;
		public int []number_progs_per_tactical;
		public static int[] tactical_first_unit;
		public string[][] args_programs;
		public int [][]num_units_program;

		public static Dictionary <int, ITaskPort<IVerifyPortType>> verify_actions;

		public override void main()
		{

			verify_actions.Add (1, Verify1);
			verify_actions.Add (2, Verify2);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			instantiateVectors();
			sendDataToTactical ();

			performOrchestration ();

			finishCertifier ();







			sw.Stop();




			Console.WriteLine("Total time of verification={0}",sw.Elapsed);


	




		}
		public void instantiateVectors(){
			

			Console.WriteLine ("number of procs " + this.Size);

		    fillInputData ();


			number_progs_per_unit_tactical = new int[number_tacticals][];

			slice_size_tacticals=new int[number_tacticals];

			tactical_first_unit = new int[number_tacticals];

			int num_progs_aux; int index; int index_first_unit=1;

			for(int i=0;i<number_tacticals;i++){
				number_progs_per_unit_tactical[i]=new int[number_units_tacticals[i]];
				slice_size_tacticals[i] = (int) Math.Floor( (double)number_progs_per_tactical[i] / number_units_tacticals[i]);
				tactical_first_unit [i] = index_first_unit;
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


		}
		public void sendDataToTactical (){

			int number_prog_read;

			index_first_prog_unit_tactical = new int[number_tacticals][];
			arr_transfer_tac = new string[number_tacticals][];
			arr_int_transfer_tac = new int[number_tacticals][];

			for (int i = 0; i < number_tacticals; i++) {
				
				arr_transfer_tac[i] = new string[slice_size_tacticals[i] + 1];
				arr_int_transfer_tac[i] = new int[slice_size_tacticals[i] + 1];
				index_first_prog_unit_tactical[i]=new int[number_units_tacticals[i]];
				number_prog_read = 0;
				for (int j = 0; j < number_units_tacticals[i]; j++) {

					if(j==1){Verify_data1.Client.setNumProgs (tactical_first_unit[i]+j, number_progs_per_unit_tactical [i][j]);
					}else{
						Verify_data2.Client.setNumProgs (tactical_first_unit[i]+j, number_progs_per_unit_tactical [i][j]);
					}

					//Communicator.world.Send<int> (number_progs_per_unit_tactical [i][j], tactical_first_unit[i]+j , dataCertifierTactical);

					//Communicator.world.Send<int> (status_verification[i], tactical_first_unit[i]+j , dataCertifierTactical);
					index_first_prog_unit_tactical [i][j] = number_prog_read;
					Console.WriteLine (" i " + i + " j " + j + " tactical_first_unit[i]+j " + (tactical_first_unit [i] + j)
						+ " number_progs_per_unit_tactical [i] " + number_progs_per_unit_tactical [i][j] +
						" index_first_prog_unit_tactical [i][j] " + index_first_prog_unit_tactical [i] [j]
						+" number_prog_read " +number_prog_read);

					Console.WriteLine ("proximo2");

					Array.Copy (num_units_program[i], number_prog_read, arr_int_transfer_tac[i], 0, number_progs_per_unit_tactical [i][j]);
					for (int k = 0; k < number_progs_per_unit_tactical [i][j]; k++) {
						Console.WriteLine ("unities of programs to " + (tactical_first_unit[i]+j));
						Console.WriteLine (arr_int_transfer_tac[i] [k]);
					}

					if (j == 1) {
						Verify_data1.Client.setUnitsProgs (tactical_first_unit [i] + j, ref arr_int_transfer_tac [i]);
					} else {
						Verify_data2.Client.setUnitsProgs (tactical_first_unit [i] + j, ref arr_int_transfer_tac [i]);
					
					}
						//Communicator.world.Send<int> (arr_int_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);

					Array.Copy (args_programs[i], number_prog_read, arr_transfer_tac[i], 0, number_progs_per_unit_tactical [i][j]);
					for (int k = 0; k < number_progs_per_unit_tactical [i][j]; k++) {
						Console.WriteLine ("args");
						Console.WriteLine (arr_transfer_tac[i] [k]);
					}
					//Communicator.world.Send<string> (arr_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);
					if (j == 1) {
						Verify_data1.Client.setArgsProgs (tactical_first_unit [i] + j, ref arr_transfer_tac [i]);
					} else {
						Verify_data2.Client.setArgsProgs (tactical_first_unit [i] + j, ref arr_transfer_tac [i]);
					}

					Array.Copy (programs[i], number_prog_read, arr_transfer_tac[i], 0, number_progs_per_unit_tactical [i][j]);

					for (int k = 0; k < number_progs_per_unit_tactical [i][j]; k++) {
						Console.WriteLine ("progs");
						Console.WriteLine (arr_transfer_tac[i][k]);
					}
					Console.WriteLine ("proximo");

					if (j == 1) {
						Verify_data1.Client.setProgs (tactical_first_unit [i] + j, ref arr_transfer_tac [i]);
					} else {
						Verify_data2.Client.setProgs (tactical_first_unit [i] + j, ref arr_transfer_tac [i]);
					}

					//Communicator.world.Send<string> (arr_transfer_tac[i], tactical_first_unit[i]+j, dataCertifierTactical);
					number_prog_read += number_progs_per_unit_tactical [i][j];
				}
			}

		}


	
		public void performOrchestration(){

			CertifierOrchestrationParser parser = new CertifierOrchestrationParser();
			parser.readOrchestrationXML(CertifierConstants.ORCHESTRATION_FILE_TEST);
			CertifierConsoleLogger.write(parser.getOrchestration().toString()); // get orchestration from the concrete certifier component

			variables.Add ("formal_properties", formal_properties);


			parser.getOrchestration().run(); // run orchestration to prove formal contracts


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
	
	}


}

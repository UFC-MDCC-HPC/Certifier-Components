using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certifier.C4;
using br.ufc.mdcc.hpc.shelf.certifier.Certifier;
using System.Diagnostics;
using br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortC4Impl;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using System.Threading;
using System.Xml.Schema;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl
{
	public class C4Impl : BaseC4Impl, ICertify
	{
		
		public static int number_tacticals=2;
		public static string[] tactical_names;
		public int num_programs=7;
		public static string [][]programs; 
		public int []number_progs_per_tactical;
		public string[][] args_programs;
		public int [][]num_units_program;
		public string[] arr_transfer_prop;

		
		

		public override string Orchestration {
			get {
				return null;
			}
		}

		public override void setData(){

			tactical_names = new string[number_tacticals];
			tactical_names [0] = "ISP1"; tactical_names [1] = "ISP2";



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

			Verify_data1.Client.setNumProgs (number_progs_per_tactical [0]);
			Verify_data2.Client.setNumProgs ( number_progs_per_tactical [1]);

			Verify_data1.Client.setUnitsProgs (ref num_units_program[0]);
			Verify_data2.Client.setUnitsProgs (ref num_units_program[1]);


			Verify_data1.Client.setArgsProgs (ref args_programs [0]);
			Verify_data2.Client.setArgsProgs (ref args_programs [1]);

			Verify_data1.Client.setProgs (ref programs [0]);

			Verify_data2.Client.setProgs (ref programs [1]);

	





		}



	
	}


}

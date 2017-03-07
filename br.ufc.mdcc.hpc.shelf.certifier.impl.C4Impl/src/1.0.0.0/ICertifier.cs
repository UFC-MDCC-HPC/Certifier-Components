using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ExpressionEvaluator;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl
{
	public class ICertifier
	{   public static ConcurrentDictionary <string, LogicActionInstantiate> InstantiateActions = new ConcurrentDictionary<string, LogicActionInstantiate>();
		public static ConcurrentDictionary <string, LogicActionCompute> ComputeActions = new ConcurrentDictionary<string, LogicActionCompute>();
		//public static String tacticalCommand = "echo 'teste echo tactical command'";
		//public static String tacticalCommand2 = "echo 'teste echo tactical command2'";
		public static TypeRegistry variables = new TypeRegistry();
		public static float provedVCPercent= 0;
		public static MultiKeyConcurrentDictionary <string,string, bool> formal_properties = new MultiKeyConcurrentDictionary <string, string, bool>();

		public int num_programs=7;
		//public int tacticalFirstUnit = 1;
		public int certifier = 0;//tag
		public int dataCertifierTactical = 71; //tag
		public static int verify_perform = 72; //tag
		public static int verify_conclusive=73; //tag
		public static int verify_inconclusive=74;   //tag 
		//public int max_properties_to_be_verified_per_prog=4;
		public static string[][] arr_transfer_prop;
		public static string[][] arr_transfer_tac;
		public static int[][] arr_int_transfer_tac;
		public int []slice_size_tacticals;
		public  static int[] status_verification;//tag
		//public int status_verification_programs=76;
		//public int[] properties_status;
		public static int [][]index_first_prog_unit_tactical;
		public static int [][]result_verification_unit_tactical;
		//public int[] tactical_responsible_prog;
		public static string [][]programs; 
		public static int number_tacticals=2;
		public static string[] tactical_names;
		public static int []number_units_tacticals;
		public static int [][] number_progs_per_unit_tactical;
		public int []number_progs_per_tactical;
		public static int[] tactical_first_unit;
		//public static string []arr_transfer_

		public string[][] args_programs;
		public int [][]num_units_program;
		//public int[] programs_verification_status;
		//ConcurrentDictionary<String, Object> variables = new ConcurrentDictionary<String, Object>();


			public ICertifier ()
		{
		}
	}
}


using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType
{
	public interface IVerifyDataPortClientType : BaseIVerifyDataPortClientType, 
	IEnvironmentPortType
	{  //SWC2
		void setNumProperties(int numProperties);
		void setMcrl2File (ref string mCRL2_file);
		void setPropertyFiles (ref string[] property_files);

		void setNumPropsTacticals ();
		void setIndexFirstPropTacticals ();
		void setPropertiesTacticals ();
		//C4

		void setNumProgs (int destination, int number);
		void setUnitsProgs (int destination, ref int[] num_units_program);
		void setArgsProgs (int destination, ref string[] args_programs);
		void setProgs (int destination, ref string[] programs);




	}
}
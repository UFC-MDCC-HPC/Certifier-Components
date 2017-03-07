using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType
{
	public interface IVerifyDataPortServerType : BaseIVerifyDataPortServerType,
	IEnvironmentPortTypeMultiplePartner


	{  //SWC2
		void setMcrl2File(ref string filename);
		void setNumProperties(int number);
		void setIndexMyFirstProp (int index);
		void setPropertyFiles(ref string [] files);
		//C4
		void setNumProgs (int number);
		void setUnitsProgs (ref int[] num_units_program);
		void setArgsProgs (ref string[] args_programs);
		void setProgs (ref string[] programs);
	}
}
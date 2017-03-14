using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeC4
{
	public interface IVerifyDataPortClientTypeC4 : IVerifyDataPortClientType, BaseIVerifyDataPortClientTypeC4
	{

		//C4

		void setNumProgs (int number);
		void setUnitsProgs (ref int[] num_units_program);
		void setArgsProgs (ref string[] args_programs);
		void setProgs (ref string[] programs);
	}
}
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4
{
	public interface IVerifyDataPortServerTypeC4 : BaseIVerifyDataPortServerTypeC4, IEnvironmentPortTypeMultiplePartner
	{
		

		//C4
		void setNumProgs (int number);
		void setUnitsProgs (ref int[] num_units_program);
		void setArgsProgs (ref string[] args_programs);
		void setProgs (ref string[] programs);


	}
}
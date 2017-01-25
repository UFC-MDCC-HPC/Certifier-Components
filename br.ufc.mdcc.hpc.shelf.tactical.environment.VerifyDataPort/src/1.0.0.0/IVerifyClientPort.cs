using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort
{
	public interface IVerifyClientPort<C> : BaseIVerifyClientPort<C>, IClientBase<C>
		where C:IVerifyDataPortClientType
	{
	}
}
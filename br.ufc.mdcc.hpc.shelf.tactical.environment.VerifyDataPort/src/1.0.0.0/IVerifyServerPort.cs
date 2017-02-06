using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort
{
	public interface IVerifyServerPort<S> : BaseIVerifyServerPort<S>, IServerBase<S>
		where S:IVerifyDataPortServerType
	{


	}


}
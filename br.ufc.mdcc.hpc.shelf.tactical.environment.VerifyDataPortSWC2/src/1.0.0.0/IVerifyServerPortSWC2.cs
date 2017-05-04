using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortSWC2
{
	public interface IVerifyServerPortSWC2<S> : BaseIVerifyServerPortSWC2<S>, IVerifyServerPort<S>
		where S:IVerifyDataPortServerTypeSWC2
	{
	}
}
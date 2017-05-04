using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortC4
{
	public interface IVerifyServerPortC4<S> : BaseIVerifyServerPortC4<S>, IVerifyServerPort<S>
		where S:IVerifyDataPortServerTypeC4
	{
	}
}
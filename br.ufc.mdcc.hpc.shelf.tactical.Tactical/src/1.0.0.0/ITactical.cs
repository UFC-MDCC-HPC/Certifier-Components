using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;

namespace br.ufc.mdcc.hpc.shelf.tactical.Tactical
{
	public interface ITactical<S, T> : BaseITactical<S, T>
		where S:IVerifyDataPortServerType
		where T:IVerifyPortType
	{
	}
}
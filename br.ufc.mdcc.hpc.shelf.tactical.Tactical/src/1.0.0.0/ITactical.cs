using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.qualifier.TacticalType;

namespace br.ufc.mdcc.hpc.shelf.tactical.Tactical
{
	public interface ITactical<S, N, T> : BaseITactical<S, N, T>
		where S:IVerifyDataPortServerType
		where T:IVerifyPortType
		where N:ITacticalType
	{
	}
}
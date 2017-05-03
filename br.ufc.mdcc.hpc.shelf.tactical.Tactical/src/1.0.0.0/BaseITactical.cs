/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.qualifier.TacticalType;

namespace br.ufc.mdcc.hpc.shelf.tactical.Tactical
{
	public interface BaseITactical<S, N, T> : ITacticalKind 
		where S:IVerifyDataPortServerType
		where N:ITacticalType
		where T:IVerifyPortType
	{
		IVerifyServerPort<S> Verify_data {get;}
		ITaskPort<T> Verify {get;}
	}
}
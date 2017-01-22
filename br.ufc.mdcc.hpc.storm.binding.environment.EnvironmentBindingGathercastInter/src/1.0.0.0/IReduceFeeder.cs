using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.workflow.Shuffle
{
	public interface IReduceFeeder<PF, OMK, S> : BaseIReduceFeeder<PF, OMK, S>
		where PF:IPartitionFunction<OMK>
		where OMK:IData
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}
}
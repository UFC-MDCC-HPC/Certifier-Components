using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using br.ufc.mdcc.common.Platform;

namespace br.ufc.mdcc.hpcshelf.mapreduce.workflow.Combiner
{
	public interface ICombiner<OMK, OMV, C, S, P> : BaseICombiner<OMK, OMV, C, S, P>
		where OMK:IData
		where OMV:IData
		where C:IEnvironmentPortTypeMultiplePartner
		where S:IEnvironmentPortTypeMultiplePartner
		where P:IPlatform
	{
	}
}
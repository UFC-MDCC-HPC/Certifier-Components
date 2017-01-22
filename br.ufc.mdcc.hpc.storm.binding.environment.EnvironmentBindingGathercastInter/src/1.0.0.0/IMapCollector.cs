using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.workflow.Shuffle
{
	public interface IMapCollector<C> : BaseIMapCollector<C>
		where C:IEnvironmentPortTypeMultiplePartner
	{
	}
}
/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.task.TaskPortTypeShuffle;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.workflow.Shuffle
{
	public interface BaseIMapCollector<C> : ISynchronizerKind 
		where C:IEnvironmentPortTypeMultiplePartner
	{
		IChannel Shuffler_channel {get;}
		ITaskPort<ITaskPorttypeShuffle> Task_port {get;}
	}
}
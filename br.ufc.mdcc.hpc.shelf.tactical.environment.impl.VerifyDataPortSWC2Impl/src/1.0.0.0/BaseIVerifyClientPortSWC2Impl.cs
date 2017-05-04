/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeSWC2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortSWC2;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl 
{

	public abstract class BaseIVerifyClientPortSWC2Impl: Synchronizer, BaseIVerifyClientPortSWC2
	{
		protected const int FACET_CLIENT = 0;
		protected const int FACET_SERVER = 1;

		private IChannel channel = null;
		protected IChannel Channel
		{
			get
			{
				if (this.channel == null)
					this.channel = (IChannel) Services.getPort("channel");
				return this.channel;
			}
		}

	}
}
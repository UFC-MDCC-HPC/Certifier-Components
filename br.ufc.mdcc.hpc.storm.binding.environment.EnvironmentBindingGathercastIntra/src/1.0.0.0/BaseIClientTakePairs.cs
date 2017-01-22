/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.environment.impl.EnvironmentBindingMbyNTakePairs 
{
	public abstract class BaseIClientTakePairs: Synchronizer, BaseIClientMbyN<IEnvironmentPortTypeMultiplePartner>
	{
		private IEnvironmentPortType client_port_type = null;

		protected IEnvironmentPortType Client_port_type
		{
			get
			{
				if (this.client_port_type == null)
					this.client_port_type = (IEnvironmentPortType) Services.getPort("client_port_type");
				return this.client_port_type;
			}
		}
	}
}
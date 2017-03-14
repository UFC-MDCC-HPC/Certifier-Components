/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeC4;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortC4Impl 
{
	public abstract class BaseIVerifyClientPortC4: Synchronizer, BaseIClientBase<IVerifyDataPortClientTypeC4>
	{
		/*private IEnvironmentPortType client_port_type = null;

		protected IEnvironmentPortType Client_port_type
		{
			get
			{
				if (this.client_port_type == null)
					this.client_port_type = (IEnvironmentPortType) Services.getPort("client_port_type");
				return this.client_port_type;
			}
		}*/

		private IVerifyDataPortClientTypeC4 client = null;
		public IVerifyDataPortClientTypeC4 Client { get {if (this.client == null)
				this.client = (IVerifyDataPortClientTypeC4) Services.getPort("client_port_type");
				return this.client; } }
	}
}
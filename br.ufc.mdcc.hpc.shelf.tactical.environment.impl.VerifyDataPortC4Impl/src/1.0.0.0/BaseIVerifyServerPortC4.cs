/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortC4Impl 
{
	public abstract class BaseIVerifyServerPortC4: Synchronizer, BaseIVerifyServerPort<IVerifyDataPortServerTypeC4>
	{
		/*private IEnvironmentPortType server_port_type = null;

		protected IEnvironmentPortType Server_port_type
		{
			get
			{
				if (this.server_port_type == null)
					this.server_port_type = (IEnvironmentPortType) Services.getPort("server_port_type");
				return this.server_port_type;
			}
		}*/

		private IVerifyDataPortServerTypeC4 server = default(IVerifyDataPortServerTypeC4);
		public IVerifyDataPortServerTypeC4 Server { set {	server = value; } get {if (this.server == null)
				this.server = (IVerifyDataPortServerTypeC4) Services.getPort("server_port_type");
				return this.server;}}
	}
}
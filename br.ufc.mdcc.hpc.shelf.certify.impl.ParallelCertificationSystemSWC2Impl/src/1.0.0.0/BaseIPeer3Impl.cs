/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.MCRL2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemSWC2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;

namespace br.ufc.mdcc.hpc.shelf.certify.impl.ParallelCertificationSystemSWC2Impl 
{
	public abstract class BaseIPeer3Impl: Application, BaseIPeer3
	{
		private ITaskPort<IVerifyPortType> verify3 = null;

		protected ITaskPort<IVerifyPortType> Verify3
		{
			get
			{
				if (this.verify3 == null)
					this.verify3 = (ITaskPort<IVerifyPortType>) Services.getPort("verify3");
				return this.verify3;
			}
		}
		private IMCRL2<IVerifyDataPortServerTypeSWC2> mcrl2_3 = null;

		protected IMCRL2<IVerifyDataPortServerTypeSWC2> MCRL2_3
		{
			get
			{
				if (this.mcrl2_3 == null)
					this.mcrl2_3 = (IMCRL2<IVerifyDataPortServerTypeSWC2>) Services.getPort("mcrl2_3");
				return this.mcrl2_3;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerTypeSWC2> verify_data3 = null;

		protected IVerifyServerPort<IVerifyDataPortServerTypeSWC2> Verify_data3
		{
			get
			{
				if (this.verify_data3 == null)
					this.verify_data3 = (IVerifyServerPort<IVerifyDataPortServerTypeSWC2>) Services.getPort("verify_data3");
				return this.verify_data3;
			}
		}
	}
}
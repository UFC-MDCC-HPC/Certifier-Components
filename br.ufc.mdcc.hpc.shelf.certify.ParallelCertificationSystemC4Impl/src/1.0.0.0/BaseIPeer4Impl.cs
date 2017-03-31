/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ISP;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl 
{
	public abstract class BaseIPeer4Impl: Application, BaseIPeer4
	{
		private ITaskPort<IVerifyPortType> verify4 = null;

		protected ITaskPort<IVerifyPortType> Verify4
		{
			get
			{
				if (this.verify4 == null)
					this.verify4 = (ITaskPort<IVerifyPortType>) Services.getPort("verify4");
				return this.verify4;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerTypeC4> verify_data4 = null;

		protected IVerifyServerPort<IVerifyDataPortServerTypeC4> Verify_data4
		{
			get
			{
				if (this.verify_data4 == null)
					this.verify_data4 = (IVerifyServerPort<IVerifyDataPortServerTypeC4>) Services.getPort("verify_data4");
				return this.verify_data4;
			}
		}
		private IISP<IVerifyDataPortServerTypeC4> isp_2 = null;

		protected IISP<IVerifyDataPortServerTypeC4> ISP_2
		{
			get
			{
				if (this.isp_2 == null)
					this.isp_2 = (IISP<IVerifyDataPortServerTypeC4>) Services.getPort("isp_2");
				return this.isp_2;
			}
		}
	}
}
using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ISP;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl
{
	public class IPeer4Impl : BaseIPeer4Impl, IPeer4
	{
		public override void main()
		{
			ISP_2.go ();
		}
	}
}

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ParTypes;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl
{
	public class IPeer1Impl : BaseIPeer1Impl, IPeer1
	{
		public override void main()
		{
			ParTypes_1.go ();
		}
	}
}

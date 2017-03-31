using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;
using br.ufc.mdcc.hpc.shelf.certifier.C4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl
{
	public class IRootImpl : BaseIRootImpl, IRoot
	{
		public override void main()
		{
			IC4 swc2 = (IC4) this.Services.getPort("c4");
			c4.go ();
		}
	}
}

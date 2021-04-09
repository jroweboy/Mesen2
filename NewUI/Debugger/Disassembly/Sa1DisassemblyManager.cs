﻿using Mesen.Debugger.Integration;
using Mesen.GUI;

namespace Mesen.Debugger.Disassembly
{
	public class Sa1DisassemblyManager : CpuDisassemblyManager
	{
		public override CpuType CpuType { get { return CpuType.Sa1; } }
		public override SnesMemoryType RelativeMemoryType { get { return SnesMemoryType.Sa1Memory; } }
		public override bool AllowSourceView { get { return false; } }

		public override void RefreshCode(ISymbolProvider symbolProvider, SourceFileInfo file)
		{
			this._provider = new CodeDataProvider(CpuType.Sa1);
		}

		protected override int GetFullAddress(int address, int length)
		{
			CpuState state = DebugApi.GetState<CpuState>(CpuType.Cpu);
			if(length == 4) {
				//Append current DB register to 2-byte addresses
				return (state.DBR << 16) | address;
			} else if(length == 2) {
				//Add direct register to 1-byte addresses
				return (state.D + address);
			}

			return address;
		}
	}
}
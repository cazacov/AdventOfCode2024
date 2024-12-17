namespace Day17
{
    internal class Computer
    {
        public long RegisterA;
        public long RegisterB;
        public long RegisterC;
        public int InstructionPointer;

        public List<int> Instructions = new List<int>();
        public List<int> Output = new List<int>();

        public enum Instruction
        {
            ADV = 0,
            BXL = 1,
            BST = 2,
            JNZ = 3,
            BXC = 4,
            OUT = 5,
            BDV = 6,
            CDV = 7
        }

        public void RunToEnd()
        {
            while (InstructionPointer < Instructions.Count - 1)
            {
                RunOneStep();
            }
        }

        public int RunToFirstOut()
        {
            while (InstructionPointer < Instructions.Count - 1)
            {
                RunOneStep();
                if (Output.Count > 0)
                {
                    return Output[0];
                }
            }
            return -1;
        }


        private void RunOneStep()
        {
            var opcode = Instructions[InstructionPointer];
            var instruction = (Instruction)opcode;
            long operand = Instructions[InstructionPointer+1];
            InstructionPointer += 2;

            if (!IsLiteral(instruction))
            {
                operand = ExpandCombo(operand);
            }
            ExecuteCommand(instruction, operand);
        }

        private void ExecuteCommand(Instruction instruction, long operand)
        {
            switch (instruction)
            {
                case Instruction.ADV:
                    RegisterA >>= (int)operand;
                    break;
                case Instruction.BXL:
                    RegisterB ^= operand;
                    break;
                case Instruction.BST:
                    RegisterB = operand & 0x00000007;
                    break;
                case Instruction.JNZ:
                    if (RegisterA != 0)
                    {
                        InstructionPointer = (int)operand;
                        if (InstructionPointer < 0 || InstructionPointer >= Instructions.Count - 1)
                        {
                            throw new Exception("Invalid jump to non-existing address " + operand);
                        }
                    }
                    break;
                case Instruction.BXC:
                    RegisterB ^= RegisterC;
                    break;
                case Instruction.OUT:
                    Output.Add((int)(operand & 0x00000007));
                    break;
                case Instruction.BDV:
                    RegisterB = RegisterA >> (int)operand;
                    break;
                case Instruction.CDV:
                    RegisterC = RegisterA >> (int)operand;
                    break;
            }
        }

        private long ExpandCombo(long operand)
        {
            if (operand < 4)
            {
                return operand;
            }
            if (operand == 4)
            {
                return RegisterA;
            }
            if (operand == 5)
            {
                return RegisterB;
            }
            if (operand == 6)
            {
                return RegisterC;
            }
            if (operand == 7)
            {
                throw  new Exception("Invalid operand");
            }
            return operand;
        }

        private bool IsLiteral(Instruction instruction)
        {
            return instruction == Instruction.BXL
                   || instruction == Instruction.JNZ;
        }

        public void Reset(long registerA)
        {
            RegisterA = registerA;
            RegisterB = 0;
            RegisterC = 0;
            InstructionPointer = 0;
            Output.Clear();
        }
    }
}

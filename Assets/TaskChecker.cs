using System;
using System.Collections.Generic;
using System.Linq;
using static SixBitExtensionsNamespace.SixBitExtensions;

public class TaskChecker {
    int task; const int taskAmount = 26;

    List<int> registers;
    List<int> data;
    List<int> param;

    public TaskChecker(List<int> param){
        this.task = param[0];
        this.param = param.Skip(1).ToList();
        }
    public void loadData(List<int> registers, List<int> data){
        this.registers = registers.ToList();
        this.data = data.ToList();}
    public string checkSolution(List<int> registers, List<int> data){
        switch (task%taskAmount){
            case 00: return TASK00(this.registers, registers, this.data, data);
            case 01: return TASK01(this.registers, registers, this.data, data);
            case 02: return TASK02(this.registers, registers, this.data, data);
            case 03: return TASK03(this.registers, registers, this.data, data);
            case 04: return TASK04(this.registers, registers, this.data, data);
            case 05: return TASK05(this.registers, registers, this.data, data);
            case 06: return TASK06(this.registers, registers, this.data, data);
            case 07: return TASK07(this.registers, registers, this.data, data);
            case 08: return TASK08(this.registers, registers, this.data, data);
            case 09: return TASK09(this.registers, registers, this.data, data);
            case 10: return TASK10(this.registers, registers, this.data, data);
            case 11: return TASK11(this.registers, registers, this.data, data);
            case 12: return TASK12(this.registers, registers, this.data, data);
            case 13: return TASK13(this.registers, registers, this.data, data);
            case 14: return TASK14(this.registers, registers, this.data, data);
            case 15: return TASK15(this.registers, registers, this.data, data);
            case 16: return TASK16(this.registers, registers, this.data, data);
            case 17: return TASK17(this.registers, registers, this.data, data);
            case 18: return TASK18(this.registers, registers, this.data, data);
            case 19: return TASK19(this.registers, registers, this.data, data);
            case 20: return TASK20(this.registers, registers, this.data, data);
            case 21: return TASK21(this.registers, registers, this.data, data);
            case 22: return TASK22(this.registers, registers, this.data, data);
            case 23: return TASK23(this.registers, registers, this.data, data);
            case 24: return TASK24(this.registers, registers, this.data, data);
            case 25: return TASK25(this.registers, registers, this.data, data);
            //case 26: return TASK26(this.registers, registers, this.data, data);
            //case 27: return TASK27(this.registers, registers, this.data, data);
            //case 28: return TASK28(this.registers, registers, this.data, data);
            //case 29: return TASK29(this.registers, registers, this.data, data);
            //case 30: return TASK30(this.registers, registers, this.data, data);
            //case 31: return TASK31(this.registers, registers, this.data, data);
            //case 32: return TASK32(this.registers, registers, this.data, data);
            //case 33: return TASK33(this.registers, registers, this.data, data);
            //case 34: return TASK34(this.registers, registers, this.data, data);
            //case 35: return TASK35(this.registers, registers, this.data, data);
            //case 36: return TASK36(this.registers, registers, this.data, data);
            //case 37: return TASK37(this.registers, registers, this.data, data);
            //case 38: return TASK38(this.registers, registers, this.data, data);
            //case 39: return TASK39(this.registers, registers, this.data, data);
            //case 40: return TASK40(this.registers, registers, this.data, data);
            //case 41: return TASK41(this.registers, registers, this.data, data);
            //case 42: return TASK42(this.registers, registers, this.data, data);
            //case 43: return TASK43(this.registers, registers, this.data, data);
            //case 44: return TASK44(this.registers, registers, this.data, data);
            //case 45: return TASK45(this.registers, registers, this.data, data);
            //case 46: return TASK46(this.registers, registers, this.data, data);
            //case 47: return TASK47(this.registers, registers, this.data, data);
            //case 48: return TASK48(this.registers, registers, this.data, data);
            //case 49: return TASK49(this.registers, registers, this.data, data);
            //case 50: return TASK50(this.registers, registers, this.data, data);
            //case 51: return TASK51(this.registers, registers, this.data, data);
            //case 52: return TASK52(this.registers, registers, this.data, data);
            //case 53: return TASK53(this.registers, registers, this.data, data);
            //case 54: return TASK54(this.registers, registers, this.data, data);
            //case 55: return TASK55(this.registers, registers, this.data, data);
            //case 56: return TASK56(this.registers, registers, this.data, data);
            //case 57: return TASK57(this.registers, registers, this.data, data);
            //case 58: return TASK58(this.registers, registers, this.data, data);
            //case 59: return TASK59(this.registers, registers, this.data, data);
            //case 60: return TASK60(this.registers, registers, this.data, data);
            //case 61: return TASK61(this.registers, registers, this.data, data);
            //case 62: return TASK62(this.registers, registers, this.data, data);
            //case 63: return TASK63(this.registers, registers, this.data, data);
            default: return "";
        }}

    static string Reg(List<int> newRegisters, int index, int expected) =>
        newRegisters[index] == expected ? "" :
        $"Expected R{index} = ☼{expected}, ☼{newRegisters[index]} found.";

    static string Dat(List<int> newData, int index, int expected) =>
        newData[index] == expected ? "" :
        $"Expected D{index} = ☼{expected}, ☼{newData[index]} found.";

    static string FirstError(params string[] errors) =>
        errors.FirstOrDefault(e => e != "") ?? "";
    
    public string describe(){
        int t = task % taskAmount;
        return $"Task {t}: {describeBody(t)}";
    }
    
    static string Oct(int v) => $"☼{decToOct(v, 2)}";
    string Rn(int i) => "R" + (param[i] % 8);              // регистр
    string An(int i) => Oct(param[i] % 64);                 // адрес
    string Vn(int i) => Oct(param[i] % 64);                 // константа
    string Nn(int i) => Oct(param[i] % 64);                 // количество
    string Dn(int i) => $"Data[{An(i)}]";                   // байт DATA
    string Bytes(int n, int a) => $"{Nn(n)} bytes of DATA starting at {An(a)}";
    
    string describeBody(int t){
        switch (t){
            case 0: return $"Rearrange the values of [{Rn(0)}, {Rn(1)}, {Rn(2)}] so that {Rn(0)} contains the smallest value, {Rn(2)} - the largest value, {Rn(1)} - the remaining value.";
            case 1: return $"Set {Rn(3)} to the median of [{Rn(0)}, {Rn(1)}, {Rn(2)}].";
            case 2: return $"Set {Rn(1)} to the number of Collatz conjecture mod ☼100 steps until {Rn(0)} gets to ☼01. In case of an infinite number of steps, set {Rn(1)} to ☼77.";
            case 3: return $"Set {Rn(2)} to |{Rn(0)} - {Rn(1)}|, {Rn(3)} to ({Rn(0)} + {Rn(1)}) mod ☼100, and {Rn(4)} to ☼01 if {Rn(0)} + {Rn(1)} is even, otherwise to ☼00.";
            case 4: return $"Rotate the 6-bit value of {Rn(0)} by {Oct(param[1] % 6)} bits ({Oct(param[1] % 6)} is in range ☼00-☼05): to the left if {Vn(2)} is even, to the right if {Vn(2)} is odd.";
            case 5: return $"Set {Rn(1)} to the number of '1' bits in {Rn(0)}.";
            case 6: return $"Set {Rn(1)} to ☼01 if {Rn(0)} is a power of two (☼01, ☼02, ☼04, ☼10, ☼20 or ☼40), otherwise set {Rn(1)} to ☼00.";
            case 7: return $"Set {Dn(2)} to ({Dn(0)} + {Dn(1)}) mod ☼100. Set {Rn(3)} to ☼01 if {Dn(0)} + {Dn(1)} is at least ☼100 (carry occurred), otherwise to ☼00.";
            case 8: return $"If {Rn(0)} > {Rn(1)}, swap the values of {Rn(0)} and {Rn(1)}. Otherwise, leave them unchanged.";
            case 9: return $"Set {Rn(2)} to ☼01 if {Dn(0)} > {Vn(1)}, to ☼00 if {Dn(0)} = {Vn(1)}, and to ☼77 (-1) if {Dn(0)} < {Vn(1)}.";
            case 10: return $"Set {Rn(2)} to the sum of {Bytes(1,0)}, modulo ☼100.";
            case 11: return $"Set {Rn(2)} to the maximum value among {Bytes(1,0)}.";
            case 12: return $"Set {Rn(3)} to the address of the first byte equal to {Vn(2)} among {Bytes(1,0)}. If there is no such byte, set {Rn(3)} to ☼77.";
            case 13: return $"For each of {Bytes(1,0)}: set the byte to {Vn(2)} if its address is even, or to {Vn(3)} if its address is odd.";
            case 14: return $"Reverse the order of bits in each of {Bytes(1,0)} (bit 0 <-> bit 5, bit 1 <-> bit 4, bit 2 <-> bit 3).";
            case 15: return $"Split {Bytes(1,0)} into consecutive pairs (1st and 2nd, 3rd and 4th, ...). Set {Rn(2)} to the sum of |x - y| over all pairs, modulo ☼100, where x and y are the bytes of a pair. If the number of bytes is odd, the last byte has no pair and is added as is.";
            case 16: return $"Copy {Bytes(1,0)} to {Nn(1)} bytes starting at {An(2)} in reverse order: the last byte of the source becomes the first byte of the destination, and so on.";
            case 17: return $"Set {Rn(2)} to ☼01 if {Oct(2 + param[1] % 3)} bytes of DATA starting at {An(0)} are sorted in non-descending order, otherwise to ☼00.";
            case 18: {
                int L = Math.Min(param[1] % 64, param[2] % 64), H = Math.Max(param[1] % 64, param[2] % 64);
                return $"Let L = {Oct(L)} be the smaller and H = {Oct(H)} the larger of {Vn(1)} and {Vn(2)}. Set {Rn(3)} to ☼00 if {Rn(0)} < {Oct(L)}, to ☼01 if {Oct(L)} <= {Rn(0)} <= {Oct(H)}, and to ☼02 if {Rn(0)} > {Oct(H)}.";
            }
            case 19: return $"Among {Bytes(1,0)}, count the local maxima: bytes that are not the first or the last byte of the range and are strictly greater than both of their neighbours. Set {Rn(2)} to that count. If the number of bytes is less than ☼03, set {Rn(2)} to ☼00.";
            case 20: return $"Replace each of {Bytes(1,0)} with its remainder of division by {Vn(2)}.";
            case 21: return $"Set {Rn(2)} to the average of {Bytes(1,0)}, rounded down. The sum is not reduced modulo ☼100.";
            case 22: return $"Add {Nn(2)} bytes of DATA starting at {An(1)} to {Nn(2)} bytes starting at {An(0)} element by element (1st to 1st, 2nd to 2nd, ...), modulo ☼100. The results replace the bytes starting at {An(0)}.";
            case 23: return $"Set {Rn(2)} to the number of bytes among {Bytes(1,0)} that are strictly greater than the average of those {Nn(1)} bytes (rounded down).";
            case 24: return $"Sort {Bytes(1,0)} in non-descending order, in place.";
            case 25: return $"Shift {Bytes(1,0)} one position to the right: each byte moves to the next address, and the byte at {An(0)} becomes {Vn(2)}. The byte that was last in the range (before the shift) must be saved to {Rn(3)}.";
            default: return "(no description)";
        }
    }
 
    
 
    string TASK00(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        //given 3 registers. sort ra, rb, rc and put them in the same order
        List<int> indices = param.Take(3).Select(x => x%8).ToList();
        List<int> sortedIndices = indices.OrderBy(x => oldRegisters[x]).ToList();
        return FirstError(Enumerable.Range(0,3)
            .Select(x => Reg(newRegisters, indices[x], oldRegisters[sortedIndices[x]])).ToArray());
    }
    string TASK01(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        //given 3 registers and rd. rd must equal median of ra, rb, rc
        List<int> indices = param.Take(3).Select(x => x%8).ToList();
        int finalIndex = param[3] % 8;
        int median = indices.Select(x => oldRegisters[x]).OrderBy(x => x).ToList()[1];
        return Reg(newRegisters, finalIndex, median);
    }
    string TASK02(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // amount of 3n+1 %64 steps; where x=1 leads to 0; x=0 leads to -1.
        List<int> answers = new List<int>{63, 0, 1, 7, 2, 5, 8, 16, 3, 19, 6, 14, 9, 9, 17, 11, 4, 12, 20, 13, 7, 63, 15, 9, 10, 10, 10, 21, 18, 11, 12, 13, 5, 22, 13, 63, 21, 12, 14, 23, 8, 14, 63, 2, 16, 4, 10, 18, 11, 8, 11, 11, 11, 6, 22, 15, 19, 17, 12, 12, 13, 20, 14, 15};
        return Reg(newRegisters, param[1]%8, answers[oldRegisters[param[0]%8]]);
    }
    string TASK03(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        //rc = abs(ra-rb), rd = ra+rb; re = (ra+rb+1)%2;
        int ra = oldRegisters[param[0]%8], rb = oldRegisters[param[1]%8];
        return FirstError(
            Reg(newRegisters, param[2]%8, ra<rb ? rb-ra : ra-rb),
            Reg(newRegisters, param[3]%8, (ra+rb)%64),
            Reg(newRegisters, param[4]%8, (ra+rb+1)%2));
    }
    string TASK04(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int registerIndex = param[0]%8, N = param[1]%6; bool D = param[2]%2 == 1;
        int ans = oldRegisters[registerIndex];
        ans = (D? (ans * 65) >> N : (ans << N) + (ans >> (6-N))) & 63;
        return Reg(newRegisters, registerIndex, ans);
    }
    string TASK05(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // count ones in Ra, put the ans in Rb.
        int num = oldRegisters[param[0]%8];
        int ans = 0;
        while (num > 0){
            ans += num & 1;
            num >>= 1;
        }
        return Reg(newRegisters, param[1]%8, ans);
    }
    string TASK06(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int ans = new List<int>{1,2,4,8,16,32}.Contains(oldRegisters[param[0]%8]) ? 1 : 0;
        return Reg(newRegisters, param[1]%8, ans);
    }
    string TASK07(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // da+db = dc, carry to rd.
        int sum = oldData[param[0]] + oldData[param[1]];
        return FirstError(
            Dat(newData, param[2], sum%64),
            Reg(newRegisters, param[3]%8, sum>63 ? 1 : 0));
    }
    string TASK08(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // if ra > rb, swap them
        int da = param[0]%8, db = param[1]%8;
        if (oldRegisters[da] > oldRegisters[db])
            return FirstError(
                Reg(newRegisters, da, oldRegisters[db]),
                Reg(newRegisters, db, oldRegisters[da]));
        else
            return FirstError(
                Reg(newRegisters, da, oldRegisters[da]),
                Reg(newRegisters, db, oldRegisters[db]));
    }
    string TASK09(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // if da > v, rc = 1; if da = v, rc = 0; if da < v, rc = -1.
        int da = oldData[param[0]], v = param[1];
        int ans;
        if (da > v) ans = 1;
        else if (da == v) ans = 0;
        else ans = 63;
        return Reg(newRegisters, param[2]%8, ans);
    }
    string TASK10(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1], rc = param[2]%8;
        int ans = 0;
        while (N>0){
            ans += oldData[aa];
            aa = (aa + 1) % 64;
            N--;
        }
        return Reg(newRegisters, rc, ans % 64);
    }
    string TASK11(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1], rc = param[2]%8;
        int ans = 0;
        while (N>0){
            if (ans < oldData[aa]) ans = oldData[aa];
            aa = (aa + 1) % 64;
            N--;
        }
        return Reg(newRegisters, rc, ans);
    }
    string TASK12(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1], V = param[2], rd = param[3]%8;
        int ans = 63;
        while (N>0){
            if (oldData[aa] == V) {
                ans = aa;
                break;
            }
            aa = (aa + 1) % 64;
            N--;
        }
        return Reg(newRegisters, rd, ans);
    }
    string TASK13(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1], Vc = param[2], Vd = param[3];
        while (N>0){
            string err = Dat(newData, aa, (aa%2==0) ? Vc : Vd);
            if (err != "") return err;
            aa = (aa + 1) % 64;
            N--;
        }
        return "";
    }
    string TASK14(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1];
        while (N>0){
            string err = Dat(newData, aa, reverseNumber(oldData[aa]));
            if (err != "") return err;
            aa = (aa + 1) % 64;
            N--;
        }
        return "";
    }
    string TASK15(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int ans = Enumerable.Range(param[0], param[1]).Select(x => oldData[x%64]).ToList()
            .Chunk(2).Select(x => x.Count==2 ? Math.Abs(x[0]-x[1]) : x[0]).Sum() % 64;
        return Reg(newRegisters, param[2]%8, ans);
    }
    string TASK16(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // block at Ac must be the reversed block from Aa
        int Aa = param[0], N = param[1], Ac = param[2];
        var old = Enumerable.Range(Aa,N).Select(x=> oldData[x%64]).ToList();
        for (int i = 0; i < N; i++){
            string err = Dat(newData, (Ac+N-1-i)%64, old[i]);
            if (err != "") return err;
        }
        return "";
    }
    string TASK17(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // check if A..A+n) sorted by nondesc, where n is 2 + param[1]%3;
        int A = param[0], n = 2 + param[1]%3, R = param[2]%8;
        int ans = Enumerable.Range(A,n-1).All(x=>oldData[x%64]<=oldData[(x+1)%64]) ? 1 : 0;
        return Reg(newRegisters, R, ans);
    }
    string TASK18(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int R = oldRegisters[param[0]%8], L = param[1], H=param[2];
        if(L > H) {L = param[2]; H = param[1];}
        int ans = R < L ? 0 : R>H ? 2 : 1;
        return Reg(newRegisters, param[3]%8, ans);
    }
    string TASK19(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int ans = (param[1] < 3) ? 0 :
            Enumerable.Range(param[0],param[1]-1).Select(x=>x%64).Skip(1)
                .Count(x=>oldData[x]>oldData[(x+63)%64] && oldData[x] > oldData[(x+1)%64]);
        return Reg(newRegisters, param[2]%8, ans);
    }
    string TASK20(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        foreach (int x in Enumerable.Range(param[0],param[1]).Select(x=>x%64)){
            string err = Dat(newData, x, oldData[x]%param[2]);
            if (err != "") return err;
        }
        return "";
    }
    string TASK21(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int ans = Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).Sum() / param[1];
        return Reg(newRegisters, param[2]%8, ans);
    }
    string TASK22(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        for (int i = 0; i < param[2]; i++){
            int idx = (param[0]+i)%64;
            string err = Dat(newData, idx, (oldData[idx]+oldData[(param[1]+i)%64])%64);
            if (err != "") return err;
        }
        return "";
    }
    string TASK23(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int avg = Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).Sum() / param[1];
        int ans = Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).Count(x=>x>avg);
        return Reg(newRegisters, param[2]%8, ans);
    }
    string TASK24(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        List<int> oldSorted = Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).OrderBy(x=>x).ToList();
        List<int> idxs = Enumerable.Range(param[0],param[1]).Select(x=>x%64).ToList();
        for (int i = 0; i < idxs.Count; i++){
            string err = Dat(newData, idxs[i], oldSorted[i]);
            if (err != "") return err;
        }
        return "";
    }
    string TASK25(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // shift block right by one, insert V at A, last old element goes to R
        int A = param[0], N = param[1], V = param[2], R = param[3]%8;
        foreach (int x in Enumerable.Range(A,N-1).Select(x=>x%64)){
            string err = Dat(newData, (x+1)%64, oldData[x]);
            if (err != "") return err;
        }
        return FirstError(
            Dat(newData, A%64, V),
            Reg(newRegisters, R, oldData[(A+N-1)%64]));
    }
}

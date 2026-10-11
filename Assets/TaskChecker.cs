using System;
using System.Collections.Generic;
using System.Linq;
using static SixBitExtensionsNamespace.SixBitExtensions;

public class TaskChecker {
    int task; const int taskAmount = 26;

    List<int> registers;
    List<int> data;
    List<int> param;

    public TaskChecker(int task){this.task = task;}
    public void loadData(List<int> registers, List<int> data, List<int> param){
        this.registers = registers.ToList();
        this.data = data.ToList();
        this.param = param.ToList();}
    public bool checkSolution(List<int> registers, List<int> data){
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
            default: return true;
        }}

    bool TASK00(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        //given 3 registers. sort ra, rb, rc and put them in the same order
        List<int> indices = param.Take(3).Select(x => x%8).ToList();
        List<int> sortedIndices = indices.OrderBy(x => oldRegisters[x]).ToList();
        return Enumerable.Range(0,3).All(x => newRegisters[indices[x]] == oldRegisters[sortedIndices[x]]);}
    bool TASK01(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        //given 3 registers and rd. rd must equal median of ra, rb, rc
        List<int> indices = param.Take(3).Select(x => x%8).ToList();
        int finalIndex = param[3] % 8;
        return newRegisters[finalIndex] == indices.Select(x=> oldRegisters[x]).OrderBy(x=>x).ToList()[1];}
    bool TASK02(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // amount of 3n+1 %64 steps; where x=1 leads to 0; x=0 leads to -1.
        List<int> answers = new List<int>{63, 0, 1, 7, 2, 5, 8, 16, 3, 19, 6, 14, 9, 9, 17, 11, 4, 12, 20, 13, 7, 63, 15, 9, 10, 10, 10, 21, 18, 11, 12, 13, 5, 22, 13, 63, 21, 12, 14, 23, 8, 14, 63, 2, 16, 4, 10, 18, 11, 8, 11, 11, 11, 6, 22, 15, 19, 17, 12, 12, 13, 20, 14, 15};
        return newRegisters[param[1]%8] == answers[oldRegisters[param[0]%8]];}
    bool TASK03(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        //rc = abs(ra-rb), rd = ra+rb; re = (ra+rb+1)%2;
        int ra = oldRegisters[param[0]%8], rb = oldRegisters[param[1]%8];
        return  newRegisters[param[2]%8] == (ra<rb?rb-ra:ra-rb) &&
                newRegisters[param[3]%8] == (ra+rb)%64 &&
                newRegisters[param[4]%8] == (ra+rb+1)%2;}
    bool TASK04(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int registerIndex = param[0]%8, N = param[1]%6; bool D = param[2]%2 == 1;
        int ans = oldRegisters[registerIndex];
        ans = (D? (ans * 65) >> N : (ans << N) + (ans >> (6-N))) & 63;
        return newRegisters[registerIndex] == ans;}
    bool TASK05(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // count ones in Ra, put the ans in Rb.
        int num = oldRegisters[param[0]%8];
        int ans = 0;
        while (num > 0){
            ans += num & 1;
            num >>= 1;
        }
        return newRegisters[param[1]%8] == ans;}
    bool TASK06(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        return newRegisters[param[1]%8] == (new List<int>{1,2,4,8,16,32}.Contains(oldRegisters[param[0]%8])?1:0);}
    bool TASK07(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // da+db = dc, carry to rd.
        return  newData[param[2]] == (oldData[param[0]] + oldData[param[1]])%64 &&
                newRegisters[param[3]%8] == ((oldData[param[0]] + oldData[param[1]])>63?1:0);}
    bool TASK08(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int da = param[0]%8, db = param[1]%8;
        if (oldRegisters[da] > oldRegisters[db]) 
             return newRegisters[da] == oldRegisters[db] && newRegisters[db] == oldRegisters[da];
        else return newRegisters[da] == oldRegisters[da] && newRegisters[db] == oldRegisters[db];}
    bool TASK09(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // if da > v, rc = 1; if da = v, rc = 0; if da < v, rc = -1.
        int da = oldData[param[0]], v = param[1];
        int ans;
        if (da > v) ans = 1;
        else if (da == v) ans = 0;
        else ans = 63;
        return newRegisters[param[2]%8] == ans;}
    bool TASK10(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1], rc = param[2]%8;
        int ans = 0;
        while (N>0){
            ans += oldData[aa];
            aa = (aa + 1) % 64;
            N--;
        }
        return newRegisters[rc] == ans % 64;}
    bool TASK11(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1], rc = param[2]%8;
        int ans = 0;
        while (N>0){
            if (ans < oldData[aa]) ans = oldData[aa];
            aa = (aa + 1) % 64;
            N--;
        }
        return newRegisters[rc] == ans;}
    bool TASK12(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
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
        return newRegisters[rd] == ans;}
    bool TASK13(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1], Vc = param[2], Vd = param[3];
        while (N>0){
            if (newData[aa]!= ((aa%2==0)?Vc:Vd)) return false;
            aa = (aa + 1) % 64;
            N--;
        }
        return true;}
    bool TASK14(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int aa = param[0], N = param[1];
        while (N>0){
            if (newData[aa]!= reverseNumber(oldData[aa])) return false;
            aa = (aa + 1) % 64;
            N--;
        }
        return true;}
    bool TASK15(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        return newRegisters[param[2]%8] == Enumerable.Range(param[0], param[1]).Select(x => oldData[x%64]).ToList()
            .Chunk(2).Select(x => x.Count==2?Math.Abs(x[0]-x[1]):x[0]).Sum() % 64;}
    bool TASK16(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int Aa = param[0], N = param[1], Ac = param[2];
        var old = Enumerable.Range(Aa,N).Select(x=> oldData[x%64]).ToList();
        var new_ = Enumerable.Range(Ac,N).Select(x=> newData[x%64]).Reverse().ToList();
        return old.Select((x,i)=>x == new_[i]).All(x=>x);}
    bool TASK17(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        // check if A..A+n) sorted by nondesc, where n is 2 + param[1]%3;
        int A = param[0], n = 2 + param[1]%3, R = param[2]%8;
        return newRegisters[R]==(Enumerable.Range(A,n-1).All(x=>oldData[x%64]<=oldData[(x+1)%64])?1:0);}
    bool TASK18(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int R = oldRegisters[param[0]%8], L = param[1], H=param[2];
        if(L > H) {L = param[2]; H = param[1];}
        int ans = R < L?0:R>H?2:1;
        return newRegisters[param[3]%8] == ans;}
    bool TASK19(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        return (param[1] < 3)? newRegisters[param[2]%8] == 0:
            Enumerable.Range(param[0],param[1]-1).Select(x=>x%64).Skip(1).Count(x=>oldData[x]>oldData[(x+63)%64] && oldData[x] > oldData[(x+1)%64]) == newRegisters[param[2]%8];}
    bool TASK20(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        return Enumerable.Range(param[0],param[1]).Select(x=>x%64).All(x=>oldRegisters[x]%param[2]==newRegisters[x]);}
    bool TASK21(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        return newRegisters[param[2]%8] == Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).Sum() / param[1];}
    bool TASK22(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        return Enumerable.Range(0,param[2]).All(i => newData[(param[0]+i)%64] == (oldData[(param[0]+i)%64]+oldData[(param[1]+i)%64])%64);}
    bool TASK23(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int avg = Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).Sum() / param[1];
        return Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).Count(x=>x>avg) == newRegisters[param[2]%8];}
    bool TASK24(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        List<int> oldSorted = Enumerable.Range(param[0],param[1]).Select(x=>oldData[x%64]).OrderBy(x=>x).ToList();
        return Enumerable.Range(param[0],param[1]).Select(x=>x%64).Select((x,i)=>newData[x] == oldSorted[i]).All(x=>x);}
    bool TASK25(List<int> oldRegisters, List<int> newRegisters, List<int> oldData, List<int> newData){
        int A = param[0], N = param[1], V = param[2], R = param[3]%8;
        return Enumerable.Range(A,N-1).Select(x=>x%64).All(x=>newRegisters[(x+1)%64] == oldRegisters[x]) &&
            newRegisters[A] == V && oldRegisters[(A+N-1)%64] == newRegisters[R];}
}

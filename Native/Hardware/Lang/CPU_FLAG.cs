namespace Yannick.Native.Hardware.Lang
{
    public enum CPU_FLAG
    {
        /* %eax */
        AVX512BF16 /*= (1 << 5)*/,

        /* %ecx */
        SSE3 /*= (1 << 0)*/,
        PCLMUL /*= (1 << 1)*/,
        LZCNT /*= (1 << 5)*/,
        SSSE3 /*= (1 << 9)*/,
        FMA /*= (1 << 12)*/,
        CMPXCHG16B /*= (1 << 13)*/,
        SSE4_1 /*= (1 << 19)*/,
        SSE4_2 /*= (1 << 20)*/,
        MOVBE /*= (1 << 22)*/,
        POPCNT /*= (1 << 23)*/,
        AES /*= (1 << 25)*/,
        XSAVE /*= (1 << 26)*/,
        OSXSAVE /*= (1 << 27)*/,
        AVX /*= (1 << 28)*/,
        F16C /*= (1 << 29)*/,
        RDRND /*= (1 << 30)*/,

        /* %edx */
        CMPXCHG8B /*= (1 << 8)*/,
        CMOV /*= (1 << 15)*/,
        MMX /*= (1 << 23)*/,
        FXSAVE /*= (1 << 24)*/,
        SSE /*= (1 << 25)*/,
        SSE2 /*= (1 << 26)*/,

        /* Extended Features (%eax == 0x80000001) */
        /* %ecx */
        LAHF_LM /*= (1 << 0)*/,
        ABM /*= (1 << 5)*/,
        SSE4a /*= (1 << 6)*/,
        PRFCHW /*= (1 << 8)*/,
        XOP /*= (1 << 11)*/,
        LWP /*= (1 << 15)*/,
        FMA4 /*= (1 << 16)*/,
        TBM /*= (1 << 21)*/,
        MWAITX /*= (1 << 29)*/,

        /* %edx */
        MMXEXT /*= (1 << 22)*/,
        LM /*= (1 << 29)*/,
        _3DNOWP /*= (1 << 30)*/,
        _3DNOW /*= (1u << 31)*/,

        /* %ebx  */
        CLZERO /*= (1 << 0)*/,
        WBNOINVD /*= (1 << 9)*/,

        /* Extended Features (%eax == 7) */
        /* %ebx */
        FSGSBASE /*= (1 << 0)*/,
        SGX /*= (1 << 2)*/,
        BMI /*= (1 << 3)*/,
        HLE /*= (1 << 4)*/,
        AVX2 /*= (1 << 5)*/,
        BMI2 /*= (1 << 8)*/,
        RTM /*= (1 << 11)*/,
        MPX /*= (1 << 14)*/,
        AVX512F /*= (1 << 16)*/,
        AVX512DQ /*= (1 << 17)*/,
        RDSEED /*= (1 << 18)*/,
        ADX /*= (1 << 19)*/,
        AVX512IFMA /*= (1 << 21)*/,
        CLFLUSHOPT /*= (1 << 23)*/,
        CLWB /*= (1 << 24)*/,
        AVX512PF /*= (1 << 26)*/,
        AVX512ER /*= (1 << 27)*/,
        AVX512CD /*= (1 << 28)*/,
        SHA /*= (1 << 29)*/,
        AVX512BW /*= (1 << 30)*/,
        AVX512VL /*= (1u << 31)*/,

        /* %ecx */
        PREFETCHWT1 /*= (1 << 0)*/,
        AVX512VBMI /*= (1 << 1)*/,
        PKU /*= (1 << 3)*/,
        OSPKE /*= (1 << 4)*/,
        WAITPKG /*= (1 << 5)*/,
        AVX512VBMI2 /*= (1 << 6)*/,
        SHSTK /*= (1 << 7)*/,
        GFNI /*= (1 << 8)*/,
        VAES /*= (1 << 9)*/,
        AVX512VNNI /*= (1 << 11)*/,
        VPCLMULQDQ /*= (1 << 10)*/,
        AVX512BITALG /*= (1 << 12)*/,
        AVX512VPOPCNTDQ /*= (1 << 14)*/,
        RDPID /*= (1 << 22)*/,
        MOVDIRI /*= (1 << 27)*/,
        MOVDIR64B /*= (1 << 28)*/,
        CLDEMOTE /*= (1 << 25)*/,

        /* %edx */
        AVX5124VNNIW /*= (1 << 2)*/,
        AVX5124FMAPS /*= (1 << 3)*/,
        IBT /*= (1 << 20)*/,
        PCONFIG /*= (1 << 18)*/,

        /* XFEATURE_ENABLED_MASK register bits (%eax == 13, %ecx == 0) */
        BNDREGS /*= (1 << 3)*/,
        BNDCSR /*= (1 << 4)*/,

        /* Extended State Enumeration Sub-leaf (%eax == 13, %ecx == 1) */
        XSAVEOPT /*= (1 << 0)*/,
        XSAVEC /*= (1 << 1)*/,
        XSAVES /*= (1 << 3)*/,

        /* PT sub leaf (%eax == 14, %ecx == 0) */
        /* %ebx */
        PTWRITE /*= (1 << 4)*/,
    }
}
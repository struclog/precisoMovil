; ModuleID = 'obj\Release\130\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Release\130\android\marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [124 x i32] [
	i32 34715100, ; 0: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 46
	i32 39109920, ; 1: Newtonsoft.Json.dll => 0x254c520 => 9
	i32 57263871, ; 2: Xamarin.Forms.Core.dll => 0x369c6ff => 41
	i32 134690465, ; 3: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 50
	i32 172012715, ; 4: FastAndroidCamera.dll => 0xa40b4ab => 3
	i32 182336117, ; 5: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 37
	i32 318968648, ; 6: Xamarin.AndroidX.Activity.dll => 0x13031348 => 20
	i32 321597661, ; 7: System.Numerics => 0x132b30dd => 14
	i32 334355562, ; 8: ZXing.Net.Mobile.Forms.dll => 0x13eddc6a => 55
	i32 342366114, ; 9: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 31
	i32 442521989, ; 10: Xamarin.Essentials => 0x1a605985 => 40
	i32 450948140, ; 11: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 29
	i32 465846621, ; 12: mscorlib => 0x1bc4415d => 8
	i32 469710990, ; 13: System.dll => 0x1bff388e => 13
	i32 527452488, ; 14: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 50
	i32 548916678, ; 15: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 6
	i32 574843983, ; 16: PrecisoGps.Android => 0x22436c4f => 0
	i32 627609679, ; 17: Xamarin.AndroidX.CustomView => 0x2568904f => 27
	i32 662205335, ; 18: System.Text.Encodings.Web.dll => 0x27787397 => 16
	i32 690569205, ; 19: System.Xml.Linq.dll => 0x29293ff5 => 19
	i32 691348768, ; 20: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 52
	i32 699874425, ; 21: PrecisoGps.dll => 0x29b73c79 => 10
	i32 700284507, ; 22: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 47
	i32 720511267, ; 23: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 51
	i32 809851609, ; 24: System.Drawing.Common.dll => 0x30455ad9 => 58
	i32 865465478, ; 25: zxing.dll => 0x3395f486 => 54
	i32 928116545, ; 26: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 46
	i32 954320159, ; 27: ZXing.Net.Mobile.Forms => 0x38e1c51f => 55
	i32 955402788, ; 28: Newtonsoft.Json => 0x38f24a24 => 9
	i32 956575887, ; 29: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 51
	i32 967690846, ; 30: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 31
	i32 974778368, ; 31: FormsViewGroup.dll => 0x3a19f000 => 4
	i32 1012816738, ; 32: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 36
	i32 1035644815, ; 33: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 22
	i32 1042160112, ; 34: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 43
	i32 1052210849, ; 35: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 33
	i32 1084122840, ; 36: Xamarin.Kotlin.StdLib => 0x409e66d8 => 49
	i32 1098259244, ; 37: System => 0x41761b2c => 13
	i32 1134191450, ; 38: ZXingNetMobile.dll => 0x439a635a => 56
	i32 1275534314, ; 39: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 52
	i32 1293217323, ; 40: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 28
	i32 1365406463, ; 41: System.ServiceModel.Internals.dll => 0x516272ff => 59
	i32 1376866003, ; 42: Xamarin.AndroidX.SavedState => 0x52114ed3 => 36
	i32 1406073936, ; 43: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 24
	i32 1411638395, ; 44: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 15
	i32 1460219004, ; 45: Xamarin.Forms.Xaml => 0x57092c7c => 44
	i32 1469204771, ; 46: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 21
	i32 1592978981, ; 47: System.Runtime.Serialization.dll => 0x5ef2ee25 => 2
	i32 1622152042, ; 48: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 34
	i32 1636350590, ; 49: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 26
	i32 1639515021, ; 50: System.Net.Http.dll => 0x61b9038d => 1
	i32 1658251792, ; 51: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 45
	i32 1698840827, ; 52: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 48
	i32 1729485958, ; 53: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 23
	i32 1766324549, ; 54: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 37
	i32 1776026572, ; 55: System.Core.dll => 0x69dc03cc => 12
	i32 1788241197, ; 56: Xamarin.AndroidX.Fragment => 0x6a96652d => 29
	i32 1796167890, ; 57: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 6
	i32 1808609942, ; 58: Xamarin.AndroidX.Loader => 0x6bcd3296 => 34
	i32 1813058853, ; 59: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 49
	i32 1813201214, ; 60: Xamarin.Google.Android.Material => 0x6c13413e => 45
	i32 1867746548, ; 61: Xamarin.Essentials.dll => 0x6f538cf4 => 40
	i32 1878053835, ; 62: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 44
	i32 1904184254, ; 63: FastAndroidCamera => 0x717f8bbe => 3
	i32 1983156543, ; 64: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 48
	i32 2011961780, ; 65: System.Buffers.dll => 0x77ec19b4 => 11
	i32 2019465201, ; 66: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 33
	i32 2055257422, ; 67: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 32
	i32 2097448633, ; 68: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 30
	i32 2126786730, ; 69: Xamarin.Forms.Platform.Android => 0x7ec430aa => 42
	i32 2201107256, ; 70: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 53
	i32 2201231467, ; 71: System.Net.Http => 0x8334206b => 1
	i32 2279755925, ; 72: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 35
	i32 2341995103, ; 73: ZXingNetMobile => 0x8b98025f => 56
	i32 2475788418, ; 74: Java.Interop.dll => 0x93918882 => 5
	i32 2570120770, ; 75: System.Text.Encodings.Web => 0x9930ee42 => 16
	i32 2605712449, ; 76: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 53
	i32 2620871830, ; 77: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 26
	i32 2732626843, ; 78: Xamarin.AndroidX.Activity => 0xa2e0939b => 20
	i32 2737747696, ; 79: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 21
	i32 2766581644, ; 80: Xamarin.Forms.Core => 0xa4e6af8c => 41
	i32 2770495804, ; 81: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 47
	i32 2778768386, ; 82: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 38
	i32 2810250172, ; 83: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 24
	i32 2819470561, ; 84: System.Xml.dll => 0xa80db4e1 => 18
	i32 2853208004, ; 85: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 38
	i32 2905242038, ; 86: mscorlib.dll => 0xad2a79b6 => 8
	i32 2978675010, ; 87: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 28
	i32 3044182254, ; 88: FormsViewGroup => 0xb57288ee => 4
	i32 3111772706, ; 89: System.Runtime.Serialization => 0xb979e222 => 2
	i32 3124832203, ; 90: System.Threading.Tasks.Extensions => 0xba4127cb => 61
	i32 3204380047, ; 91: System.Data.dll => 0xbefef58f => 57
	i32 3215347189, ; 92: zxing => 0xbfa64df5 => 54
	i32 3247949154, ; 93: Mono.Security => 0xc197c562 => 60
	i32 3258312781, ; 94: Xamarin.AndroidX.CardView => 0xc235e84d => 23
	i32 3265893370, ; 95: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 61
	i32 3317135071, ; 96: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 27
	i32 3317144872, ; 97: System.Data => 0xc5b79d28 => 57
	i32 3353484488, ; 98: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 30
	i32 3353544232, ; 99: Xamarin.CommunityToolkit.dll => 0xc7e30628 => 39
	i32 3358260929, ; 100: System.Text.Json => 0xc82afec1 => 17
	i32 3362522851, ; 101: Xamarin.AndroidX.Core => 0xc86c06e3 => 25
	i32 3366347497, ; 102: Java.Interop => 0xc8a662e9 => 5
	i32 3374999561, ; 103: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 35
	i32 3395150330, ; 104: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 15
	i32 3404865022, ; 105: System.ServiceModel.Internals => 0xcaf21dfe => 59
	i32 3407215217, ; 106: Xamarin.CommunityToolkit => 0xcb15fa71 => 39
	i32 3429136800, ; 107: System.Xml => 0xcc6479a0 => 18
	i32 3476120550, ; 108: Mono.Android => 0xcf3163e6 => 7
	i32 3485117614, ; 109: System.Text.Json.dll => 0xcfbaacae => 17
	i32 3509114376, ; 110: System.Xml.Linq => 0xd128d608 => 19
	i32 3526203020, ; 111: PrecisoGps => 0xd22d968c => 10
	i32 3536029504, ; 112: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 42
	i32 3632359727, ; 113: Xamarin.Forms.Platform => 0xd881692f => 43
	i32 3641597786, ; 114: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 32
	i32 3672681054, ; 115: Mono.Android.dll => 0xdae8aa5e => 7
	i32 3689375977, ; 116: System.Drawing.Common => 0xdbe768e9 => 58
	i32 3760922309, ; 117: PrecisoGps.Android.dll => 0xe02b1ec5 => 0
	i32 3829621856, ; 118: System.Numerics.dll => 0xe4436460 => 14
	i32 3896760992, ; 119: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 25
	i32 3955647286, ; 120: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 22
	i32 4105002889, ; 121: Mono.Security.dll => 0xf4ad5f89 => 60
	i32 4151237749, ; 122: System.Core => 0xf76edc75 => 12
	i32 4260525087 ; 123: System.Buffers => 0xfdf2741f => 11
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [124 x i32] [
	i32 46, i32 9, i32 41, i32 50, i32 3, i32 37, i32 20, i32 14, ; 0..7
	i32 55, i32 31, i32 40, i32 29, i32 8, i32 13, i32 50, i32 6, ; 8..15
	i32 0, i32 27, i32 16, i32 19, i32 52, i32 10, i32 47, i32 51, ; 16..23
	i32 58, i32 54, i32 46, i32 55, i32 9, i32 51, i32 31, i32 4, ; 24..31
	i32 36, i32 22, i32 43, i32 33, i32 49, i32 13, i32 56, i32 52, ; 32..39
	i32 28, i32 59, i32 36, i32 24, i32 15, i32 44, i32 21, i32 2, ; 40..47
	i32 34, i32 26, i32 1, i32 45, i32 48, i32 23, i32 37, i32 12, ; 48..55
	i32 29, i32 6, i32 34, i32 49, i32 45, i32 40, i32 44, i32 3, ; 56..63
	i32 48, i32 11, i32 33, i32 32, i32 30, i32 42, i32 53, i32 1, ; 64..71
	i32 35, i32 56, i32 5, i32 16, i32 53, i32 26, i32 20, i32 21, ; 72..79
	i32 41, i32 47, i32 38, i32 24, i32 18, i32 38, i32 8, i32 28, ; 80..87
	i32 4, i32 2, i32 61, i32 57, i32 54, i32 60, i32 23, i32 61, ; 88..95
	i32 27, i32 57, i32 30, i32 39, i32 17, i32 25, i32 5, i32 35, ; 96..103
	i32 15, i32 59, i32 39, i32 18, i32 7, i32 17, i32 19, i32 10, ; 104..111
	i32 42, i32 43, i32 32, i32 7, i32 58, i32 0, i32 14, i32 25, ; 112..119
	i32 22, i32 60, i32 12, i32 11 ; 120..123
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}

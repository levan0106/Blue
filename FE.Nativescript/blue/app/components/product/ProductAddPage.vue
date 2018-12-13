<template>
    <page>
        <ActionBar title="Đăng tin cho thuê" background="#ffffff" borderColor="#2d5bb6" color="black">
            <NavigationButton text="Quay lại" android.systemIcon="ic_menu_back" 
                @tap="$navigateBack" />
        </ActionBar>
        <GridLayout  rows="*,auto" >
            <ScrollView row="0">
                <StackLayout class="panel-padding" >
                    <StackLayout class="border-bottom">                        
                        <label class="title">Chọn loại</label> 
                        <!-- <ScrollView orientation="horizontal" width="100%"
                        scrollBarIndicatorVisible="false">
                            <StackLayout orientation="horizontal">
                                <StackLayout v-for="(cat,index) in categories" 
                                :key="index" 
                                width="80" class="category-group-item">
                                    <Image :src="cat.Image"/>
                                    <Label class="category-item-title" :text="cat.Name"/>   
                                </StackLayout>
                            </StackLayout>
                        </ScrollView> -->
                        <category-map :data="categories" v-model="item.Category"></category-map>
                    </StackLayout >
                    <StackLayout class="border-bottom item-detail">
                        <label class="title">Thông tin cho thuê</label>
                        <label >Tên</label>
                        <TextField  hint="Tối đa 50 ký tự" returnKeyType="next"
                        v-model="item.Name" maxLength="50" />
                        <label>Giá thuê</label>
                        <!-- <TextField  hint="500,000 VNĐ" keyboardType="number" returnKeyType="next" 
                        :text="item.Price|currency"/> -->
                        <text-field-currency hint="200,000" 
                        v-model="item.Price" 
                        returnKeyType="next"></text-field-currency>
                        <label >Đặt cọc</label>
                        <!-- <TextField  hint="1,000,000 VNĐ" keyboardType="number" returnKeyType="next"
                        v-model="item.Deposit" /> -->
                        <text-field-currency hint="1,000,000" 
                        v-model="item.Deposit" 
                        returnKeyType="next"></text-field-currency>
                        <label >Cho thuê theo</label>
                        <!-- <FlexboxLayout alignItems="center" 
                        justifyContent="space-between" width="100%" >
                            <StackLayout class="btn-select-border btn-select"
                            height="100px">
                                <label text="Giờ"></label>
                            </StackLayout>
                            <StackLayout class="btn-select-border btn-select"
                            height="100px">
                                <label text="Ngày" ></label>
                            </StackLayout>
                            <StackLayout class="btn-select-border btn-select"
                            height="100px">
                                <label text="Tháng" ></label>
                            </StackLayout>
                        </FlexboxLayout> -->
                        <!-- <label :text="item.RentType.Value"></label> -->
                        <radio :data="rentTypes" v-model="item.RentType"/>

                        <label >Cho thuê từ</label>
                        <FlexboxLayout width="100%"
                        class="date-range">                            
                            <label width="40%" class="date" :text="item.FromDate|date"
                            @tap="openDateRange"></label>
                            <label width="20%" text="đến"></label>
                            <label width="40%" class="date" :text="item.ToDate|date"
                            @tap="openDateRange"></label>
                        </FlexboxLayout>
                        <label >Sản phẩm có giao?</label>
                        <radio :data="shippings" v-model="item.Shipping"/>
                        <!-- <FlexboxLayout alignItems="center" 
                        justifyContent="space-between" width="100%" >
                            <StackLayout class="btn-select-border btn-select">
                                <label text="Có giao hàng"></label>
                            </StackLayout>
                            <StackLayout class="btn-select-border btn-select">
                                <label text="Không giao hàng" ></label>
                            </StackLayout>
                        </FlexboxLayout> -->
                        <label >Thành phố</label>
                        <TextField  hint="Chọn thành phố" returnKeyType="next"
                        v-model="item.City" />
                        <label >Quận</label>
                        <TextField  hint="Chọn quận" returnKeyType="next"
                        v-model="item.State" />
                        <label >Địa chỉ</label>
                        <TextField  hint="Số nhà, tên đường, tên phường" returnKeyType="next" 
                        v-model="item.Address"/>
                        <label >Số điện thoại</label>
                        <TextField  hint="Số di động của bạn" keyboardType="phone" returnKeyType="next"
                        v-model="item.PhoneNumber" />
                    </StackLayout>
                    <StackLayout class="border-bottom">
                        <label class="title">Thông tin thêm</label>
                        <!-- <FlexboxLayout flexWrap="wrap" 
                        alignItems="center" width="100%"
                        justifyContent="space-between">
                            <StackLayout class="btn-select-border btn-select"
                            v-for="(item,index) in attributes" :key="index">
                                <label :text="item.value" ></label>
                            </StackLayout>                            
                        </FlexboxLayout> -->
                        <radio :data="attributes" v-model="item.Attribute" 
                        flex-wrap multiple/>
                    </StackLayout>
                    <StackLayout class="border-bottom">
                        <label class="title">Mô tả</label>   
                        <TextView hint="Thêm ghi chú về sản phẩm" returnKeyType="next"                        
                        v-model="item.Description"  />                     
                    </StackLayout>
                    <StackLayout class="border-bottom">
                        <label class="title">Đăng ảnh</label>  
                        <label >Tối đa 10 ảnh dung lượng max: 5MB/ảnh</label> 
                        
                        <button text="Chọn ảnh" class=" btn" @tap="chooseImage" /> 
                        <button text="Tải ảnh lên" class=" btn" @tap="takePicture" />                       
                        <FlexboxLayout flexWrap="wrap" width="100%"
                        justifyContent="flex-start">  
                            <StackLayout v-for="(image,index) in pictureSource" :key="index" >
                            <Image :src="image" width="100" height="100"/>
                            <label text="remove" @tap="removeImage(index)"></label>
                            </StackLayout>                          
                        </FlexboxLayout>  
                    </StackLayout>
                    <button text="Đăng ngay" class="btn-submit btn" @tap="onSubmit" /> 
                </StackLayout>
            </ScrollView>
            <StackLayout row="1">
                <navigation-footer></navigation-footer>  
            </StackLayout>
        </GridLayout>
    </page>
</template>

<script>
// import * as camera from "nativescript-camera";
import { Image } from "tns-core-modules/ui/image";
import * as camera from "nativescript-camera"; 
import * as imageSourceModule from "tns-core-modules/image-source"; 
import { isAndroid, isIOS } from "tns-core-modules/platform";
import { path, knownFolders } from "tns-core-modules/file-system"; 
import * as imagepicker from "nativescript-imagepicker"; 
var context = imagepicker.create({ mode: "multiple" }); // use "multiple" for multiple selection 

import NavigationFooter from '@/components/navigation/NavigationFooter.vue'
import TextFieldCurrency from "@/components/controls/TextFieldCurrency.vue"
import Radio from "@/components/controls/Radio.vue"
import CategoryMap from "@/components/controls/CategoryMap.vue"

export default {
    name:"ProductAddPage",
     components:{
        NavigationFooter,
        TextFieldCurrency,
        Radio,
        CategoryMap     
    },
    data(){
        return{
            rentTypes:[
                {Value:"Giờ", Key:1},
                {Value:"Ngày", Key:2},
                {Value:"Tháng", Key:3}
            ],
            shippings:[
                {Value:"Có giao hàng", Key:1},
                {Value:"Không giao hàng", Key:2}
            ],
            attributes:[
                {Key:1,Value:"bé trai"},
                {Key:2,Value:"bé gái"},
                {Key:3,Value:"trên 90%"},
                {Key:4,Value:"chống nước"},
                {Key:5,Value:"nhựa"}
            ],
            
            item:{
                Name:null,
                Price:null,
                Deposit:null,
                FromDate:new Date(),
                ToDate: new Date(),
                City:null,
                State:null,
                Address:null,
                PhoneNumber:null,
                Description:null,
                Images:[],
                Shipping:null,
                RentType:null,
                Attribute:[],
                Category:null
            },
            pictureSource: [], 
        }
    },
    computed:{
        categories(){
            return this.$store.getters['category/all'].filter(item=>!item.fixedPosition)
        },
    },
    created(){        
        this.$store.dispatch('category/pullAll')
    },
    mounted(){
        if (camera.isAvailable()) { 
            //checks to make sure device has a camera 
            camera.requestPermissions()
            .then( //request permissions for camera success 
                () => { 
                    //have permissions 
                }, failure => { 
                    //no permissions for camera,disable picture button? 
                    console.log('permissions rejected')
                } );
        } else { 
            //ignore this on simulators for now 
            alert('Your camera is not available.')
        } 
        
    },
    methods:{
        openDateRange(){
            this.$router.openSelectDateModal(this)
        },
        onSubmit(){

        },
        removeImage(index){
            this.pictureSource.splice(index,1);
        },
        takePicture(){                      
            var options = { width: 300, height: 300, keepAspectRatio: false, saveToGallery: true,cameraFacing:true };
            camera.takePicture(options)
            .then((imageAsset) => {
                console.log("Size: " + imageAsset.options.width + "x" + imageAsset.options.height);
                console.log("keepAspectRatio: " + imageAsset.options.keepAspectRatio);
                console.log("Photo saved in Photos/Gallery for Android or in Camera Roll for iOS");
            })
            .catch((err) => {
                console.log("Error -> " + err.message);
            });
        },
        chooseImage() { 
            try { 
                context.authorize()
                .then(() => { 
                    return context.present(); 
                }) 
                .then(selection => { 
                    selection.forEach((item,index)=>{
                        this.readImage(item);
                    })
                    //const imageAsset = selection.length > 0 ? selection[0] : null;
                    
                }).catch(err => { 
                    console.log(err); 
                }); 
            } catch (err) { 
                alert("Please select a valid image."); 
                console.log(err) 
            } 
        }, 
        readImage(imageAsset){
            imageAsset.options = { 
                        width: 400, 
                        height: 400, 
                        keepAspectRatio: true 
                    }; 
            imageSourceModule.fromAsset(imageAsset) 
            .then(imageSource => { 
                let saved = false; 
                let localPath = ""; 
                let filePath = ""; 
                let image = {}; 
                const folderPath = knownFolders.documents().path; 
                let fileName = new Date().getTime() + ".jpg"; 
                if (imageAsset.android) { 
                    localPath = imageAsset.android.toString().split("/"); 
                    fileName = fileName + "_" + localPath[localPath.length - 1].split(".")[0] + ".jpg";
                    filePath = path.join(folderPath, fileName); 
                    saved = imageSource.saveToFile(filePath, "jpeg"); 
                    if (saved) { 
                        this.pictureSource.push(imageAsset.android.toString()); 
                    } else { 
                        console.log( "Error! Unable to save pic to local file for saving" ); 
                    } 
                } else { 
                    const ios = imageAsset.ios; 
                    if (ios.mediaType === PHAssetMediaType.Image) { 
                        const opt = PHImageRequestOptions.new(); 
                        opt.version = PHImageRequestOptionsVersion.Current; 
                        PHImageManager.defaultManager()
                        .requestImageDataForAssetOptionsResultHandler( ios, opt, (imageData, dataUTI, orientation, info) => { 
                            image.src = info .objectForKey("PHImageFileURLKey") .toString(); 
                            localPath = image.src.toString().split("/"); 
                            fileName = fileName + "_" + localPath[localPath.length - 1].split(".")[0] + ".jpeg"; 
                            filePath = path.join(folderPath, fileName); 
                            saved = imageSource.saveToFile(filePath, "jpeg"); 
                            if (saved) { 
                                console.log("saved picture to " + filePath); 
                                this.pictureSource = filePath; 
                            } else { 
                                console.log( "Error! Unable to save pic to local file for saving" ); 
                            } 
                        } ); 
                    } 
                } 
            }).catch(err => { 
                console.log(err); 
            });
        }
    }
}
</script>

<style scoped>
.item-detail label{
    margin-top: 20px;
    margin-bottom: 10px;
}
.border-bottom{
    margin-bottom: 20px;
}
.date-range{
    text-align: center;
}
.date{
    border-bottom-width:1px;
    border-bottom-color: #333333; 
}
.actived{
    color: #2d5bb6;
    font-weight: bold;
    vertical-align: center;
    text-align: center;
}
/* .btn-select{
    vertical-align: middle;
    text-align: center;
    min-height: 100px;
    margin: 10px 20px;
    width: 45%;
    max-width: 200px;
} */
.category-group-item{
    padding:20px 30px;
}
.category-item-title{
    vertical-align: center;
    text-align: center;
    font-size: 14;
    color: #333333;
}
</style>

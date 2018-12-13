<template>
    <page>
        <ActionBar title="Tài khoản" background="transparent">
            <NavigationButton text="Quay lại" android.systemIcon="ic_menu_back" 
            @tap="$navigateBack" /> 
            <ActionItem @tap="onTapEdit"
                v-show="!isEditing"
                ios.systemIcon="2" ios.position="right"
                android.systemIcon="ic_menu_edit" />
            <ActionItem @tap="onTapSave"
                v-show="isEditing"
                ios.systemIcon="3" ios.position="right"
                android.systemIcon="ic_menu_save" />
            <ActionItem @tap="onTapCancel"
                v-show="isEditing"
                ios.systemIcon="1"
                android.systemIcon="ic_menu_close_clear_cancel" />
        </ActionBar>
        <GridLayout coloumns="*" rows="*,auto" > 
            <!-- product list -->
            <StackLayout
                row="0" col="0">    
                    <ScrollView class="home-panel">
                        <StackLayout>
                            <AbsoluteLayout>                                
                                <Image src="~/assets/images/banner.png" 
                                    height="170" top="0" left="0"/>
                                <StackLayout class="header-panel" top="100" left="0"
                                    height="130" width="100%">
                                    <StackLayout class="header-container"                                                                  
                                    backgroundColor="#ffffff" height="100%" width="100%" >                                
                                        <label class="title">{{user.name}}</label>
                                    </StackLayout>
                                </StackLayout>                                
                            </AbsoluteLayout>
                            <StackLayout  class="panel-padding" background="transparent">
                                <StackLayout class="panel panel-margin panel-padding">
                                    <label text="Liên hệ"></label>
                                </StackLayout>
                                <StackLayout class="panel panel-margin panel-padding">
                                    <label text="Bài viết của tôi"></label>
                                </StackLayout>
                                <StackLayout class="panel panel-margin panel-padding">
                                    <label text="Lịch sử" class="border-bottom"></label>
                                    <label text="Danh sách yêu thích" class="border-bottom"></label>
                                    <label text="Danh sách đang chờ"></label>
                                </StackLayout>
                                <StackLayout class="panel panel-margin panel-padding">
                                    <label text="Đánh giá của tôi" class="border-bottom"></label>
                                    <label text="Danh sách chưa đánh giá"></label>
                                </StackLayout>
                                <StackLayout class="panel panel-margin panel-padding">
                                    <label text="Mã giảm giá đã lưu"></label>
                                </StackLayout>
                                <StackLayout class="panel panel-margin panel-padding">
                                    <label text="Thoát" @tap="doLogout"></label>
                                </StackLayout>
                            </StackLayout>
                        </StackLayout>                            
                    </ScrollView>
            </StackLayout >
            <StackLayout col="0" row="1" class="footer">
                <navigation-footer tab-selected="profile"></navigation-footer>  
            </StackLayout>
        </GridLayout>
    </page>
</template>

<script>
import NavigationFooter from '@/components/navigation/NavigationFooter.vue'

export default {
    name:"ProfilePage",
    components:{
        NavigationFooter
    },
    data(){
        return{
            isEditing:false,
            user: this.$store.state.currentUser
        }
    },
    methods:{
        onTapEdit(){
            this.isEditing=true;
        },
        onTapSave(){
            this.isEditing=false;
        },
        onTapCancel(){
            this.isEditing=false;
        },
        doLogout(){
            this.$store.commit('save',{});
            this.$router.goToHomePage(this);
        }
    }
}
</script>

<style scoped>
.panel label{
    padding: 30px;
}
.header-container{
    width: 100%;
    padding:40px;
    border-radius: 20px;
    text-align: center;
    vertical-align: center;
}
.header-panel{
    padding:0px 40px;
    background: transparent;
}
</style>

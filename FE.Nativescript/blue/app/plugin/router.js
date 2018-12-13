import Vue from 'vue'
import HomePage from '@/components/HomePage.vue'
import LoginPage from '@/components/login/LoginPage.vue'
import ProductDetailPage from '@/components/product/ProductDetailPage.vue'
import ProductAddPage from '@/components/product/ProductAddPage.vue'
import SelectDatePage from '@/components/order/SelectDatePage.vue'
import SearchPage from '@/components/search/SearchPage.vue'
import ProfilePage from '@/components/profile/ProfilePage.vue'
import FilterPage from '@/components/search/FilterPage.vue'
import BookMarkPage from '@/components/bookmark/BookMarkPage.vue'
import SellerPage from '@/components/seller/SellerPage.vue'

const router = {
    goTo(root,page,agrs){
        root.$navigateTo(
            page=="SearchPage"?SearchPage:
            page=="ProfilePage"?funcs.checkAuthentication(root)?ProfilePage:LoginPage:
            page=="BookMarkPage"?BookMarkPage:
            page=="ProductAddPage"?ProductAddPage:
            HomePage
            ,{
            transition,    
            transitionIOS, 
            transitionAndroid,          
            props: {
               item:agrs,
               returnUrl:'ProfilePage'
            }
        })
    },  
    goToProductAddPage(root,agrs){     
        const isAuthenticated = funcs.checkAuthentication(root);   
        root.$navigateTo(
            isAuthenticated?ProductAddPage:LoginPage,{
            transition,    
            transitionIOS, 
            transitionAndroid,          
            props: {
                id:agrs,
                returnUrl:'ProductAddPage'
            }
        })
    }, 
    goToSellerPage(root,agrs){
        root.$navigateTo(SellerPage,{
            transition,    
            transitionIOS, 
            transitionAndroid,          
            props: {
                id:agrs
            }
        })
    },  
    goToFilterPage(root,agrs){
        root.$navigateTo(FilterPage,{
            transition:{},    
            transitionIOS:{}, 
            transitionAndroid:{},          
            props: {
               item:agrs
            }
        })
    },
    goToHomePage(root,agrs){
        root.$navigateTo(HomePage,{
            transition,    
            transitionIOS, 
            transitionAndroid,          
            props: {
               item:agrs
            }
        })
    },
    goToLoginPage(root,agrs){
        root.$navigateTo(LoginPage,{
            transition,    
            transitionIOS, 
            transitionAndroid,          
            props: {
                returnUrl:agrs
            }
        });
    },
    goToProductDetailPage(root,agrs){
        root.$navigateTo(ProductDetailPage,{
            transition,   
            transitionIOS, 
            transitionAndroid,       
            props: {
               item:agrs
            }
        });
    },    
    goToSearchPage(root,agrs){
        root.$navigateTo(SearchPage,{
            transition:{},   
            transitionIOS:{}, 
            transitionAndroid:{},       
            props: {
               item:agrs
            }
        });
    },
    openLoginModal(root,agrs){
        root.$showModal(LoginPage)
    },    
    openSelectDateModal(root,agrs){
        root.$showModal(SelectDatePage)
    },
}
const transition= {
        name:"slide",
        duration: 200,
        curve: 'linear'
    },
    transitionIOS= {
        
    },
    transitionAndroid= {
        name:"slide",
        duration: 200,
        curve: 'linear'
    }
const funcs={
    checkAuthentication(root){        
        //get all state
        root.$store.commit('load')

        if(root.$store.state.isAuthenticated)
            return true;
        return false;
    }
}

Vue.prototype.$router= router;
export default router
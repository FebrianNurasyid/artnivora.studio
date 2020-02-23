import React from 'react';
import { Map } from 'immutable';
import { MdFace, MdSettings, MdBusinessCenter, MdFileUpload, MdWork, MdCreate, MdFontDownload } from "react-icons/md";
import {
    VOLUNTEER,
    PARTICIPANT, 
    GUEST
} from 'hvb-shared-frontend/src/constants/roles';


export const usersSubUrls = Map({
    'registerUser': Map({
        to: '/users/register',
        label: 'Register',
        parent: 'users',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'loginUser': Map({  
        to: '/users/login',
        label: 'Login',
        parent: 'users',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
});

export const masterssSubUrls = Map({
    'users': Map({
        to: '/masters/users',
        label: 'Users',
        parent: 'masters',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'roles': Map({
        to: '/masters/roles',
        label: 'Roles',
        parent: 'masters',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'divisions': Map({
        to: '/masters/divisions',
        label: 'Divisions',
        parent: 'masters',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'category': Map({
        to: '/masters/category',
        label: 'Category',
        parent: 'masters',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'themes': Map({
        to: '/masters/themes',
        label: 'Themes',
        parent: 'masters',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
});

export const userDetailUrls = Map({
    'profile-detail': Map({
        to: '/personal-detail/userprofile',
        label: 'Personal Detail',
        parent: 'personal-detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'volunteer-profile': Map({
        to: '/personal-detail/volunteerprofile',
        label: 'Volunteer Detail',
        parent: 'personal-detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'target-audience': Map({
        to: '/personal-detail/audience',
        label: 'Target Audience',
        parent: 'personal-detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'diet': Map({
        to: '/personal-detail/diet',
        label: 'Diet',
        parent: 'personal-detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'DRUG-LIST': Map({
        to: '/personal-detail/drugs',
        label: 'Drug List',
        parent: 'personal-detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'financial': Map({
        to: '/personal-detail/financial',        
        label: 'Financial',
        parent: 'personal-detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),    
});

export const messagesSubUrls = Map({
    'messageReceived': Map({
        to: '/messages/received',
        label: 'Ontvangen',
        parent: 'messages',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'messageSent': Map({
        to: '/messages/sent',
        label: 'Verzonden',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'messageArchive': Map({
        to: '/messages/archive',
        label: 'Archief',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
});

export const sidebarUrls = Map({
    '/': Map({
        to: '/overview',
        label: 'Dashboard',
        matchExact: true,
        icon: <MdBusinessCenter />,
        disabled: false,        
    }),       
    'production': Map({
        to: '/production',
        label: 'Production',        
        matchExact: false,
        icon: <MdCreate />,
        disabled: false,
        //allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'packing': Map({
        to: '/production/packing',
        label: 'Packing',        
        matchExact: false,
        icon: <MdWork />,
        disabled: false,
        //allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'keywording': Map({
        to: '/production/keywording',
        label: 'Keywording',        
        matchExact: false,
        icon: <MdFontDownload />,
        disabled: false,
        //allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'uploading': Map({
        to: '/production/uploading',
        label: 'Uploading',        
        matchExact: false,
        icon: <MdFileUpload />,
        disabled: false,
        //allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'masters': Map({
        to: '/masters',
        label: 'Masters',
        children: masterssSubUrls,
        matchExact: false,
        icon: <MdSettings />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),   
});

export const headerUrls = Map({
    'create': Map({
        to: '/users/create',
        label: 'Register',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        //allowedRoles: [GUEST],
    }),
    'login': Map({
        to: '/users/login',
        label: 'Login',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'register': Map({
        to: '/users/register',
        label: 'Register',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        //allowedRoles: [GUEST],
    }),
    'privacy-statement': Map({
        to: '/users/privacy-statement',
        label: 'Privacy Statement',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'thankyou': Map({
        to: '/users/thankyou',
        label: 'Thank You',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'activation': Map({
        to: '/users/activation',
        label: 'Activation',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
    }),
    'userprofile': Map({
        to: '/personal-detail/userprofile',
        label: 'Personal Detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'users': Map({
        to: '/masters/users',
        label: 'Users',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'roles': Map({
        to: '/masters/roles',
        label: 'Roles',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'category': Map({
        to: '/masters/category',
        label: 'Category',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'divisions': Map({
        to: '/masters/divisions',
        label: 'Divisions',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'themes': Map({
        to: '/masters/themes',
        label: 'Themes',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),
    'volunteerprofile': Map({
        to: '/personal-detail/volunteerprofile',
        label: 'Volunteer Detail',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        allowedRoles: [VOLUNTEER, PARTICIPANT],
    }),   
    'creatediv': Map({
        to: '/masters/division',
        label: 'Add Division',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        //allowedRoles: [GUEST],
    }),
    'addoredit': Map({
        to: '/production/addoredit',
        label: 'Add or Edit Task',
        matchExact: false,
        icon: <MdFace />,
        disabled: false,
        //allowedRoles: [GUEST],
    }),    
});

export const allUrls = sidebarUrls.concat(headerUrls);
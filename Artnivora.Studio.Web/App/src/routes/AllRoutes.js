import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import ProtectedRoute from 'hvb-shared-frontend/src/components/Routes/ProtectedRoute';

import Home from '../components/Home';
import UserLogin from '../components/Users/Login/Login';
import UsersOverview from '../components/Users/Overview';
import UsersCreateOrEdit from '../components/Users/CreateOrEdit';
import UsersRegister from '../components/Users/Register';
import ProfilePage from '../components/Users/ProfilePage';
import ConfigurePassword from '../components/Masters/ConfigurePassword';
import PageNotFound from 'hvb-shared-frontend/src/components/PageNotFound/PageNotFound';
import Overview from '../components/Dashboard/Overview';
import MasterUsers from '../components/Masters/Users';
import MasterDivision from '../components/Masters/Divisions/Division';
import MasterCategory from '../components/Masters/Category';
import MasterThemes from '../components/Masters/Themes';
import DivisionCreateOrEdit from '../components/Masters/Divisions/CreateOrEdit';
import ProductionTask from '../components/Production/productionTask';
import ProductionAddOrEditTask from '../components/Production/addOrEdit';
import PackagingTask from '../components/Production/packagingTask';
import EditPackaging from '../components/Production/editPackaging';
import KeywordingTask from '../components/Production/keywordingTask';
import EditKeywording from '../components/Production/editKeywording';
import UploadingTask from '../components/Production/uploadingTask';
import EditUploading from '../components/Production/editUploading';
import MasterUsersRegister from '../components/Masters/Register';
import Brief from '../components/Brief/Brief';
import AddOrEditBrief from '../components/Brief/AddOrEditBrief';

//import ParticipantProfile from '../components/Users/ParticipantProfile';
//import PrivacyStatment from '../components/Users/PrivacyStatement';
//import ThankYou from '../components/Users/ThankYou';
//import TokenConfirmation from '../components/Users/TokenConfirmation';
//import VolunteerProfile from '../components/Users/VolunteerProfile';
//import MessageReceived from '../components/Messages/MessageReceived';
//import DetailMessage from '../components/Messages/DetailMessage';
//import CreateMessages from '../components/Messages/CreateMessages';

//<Route exact path='/users/privacy-statement' component={PrivacyStatment} />
//    <Route exact path='/users/thankyou' component={ThankYou} />
//    <Route exact path='/users/tokenconfirmation' component={TokenConfirmation} />
//    <ProtectedRoute exact path='/messages/received' component={MessageReceived} />
//    <ProtectedRoute exact path='/messages/create' component={CreateMessages} />
//    <ProtectedRoute exact path='/messages/detailmessage' component={DetailMessage} />
//    <ProtectedRoute exact path='/personal-detail/volunteerprofile' component={VolunteerProfile} />

class AllRoutes extends Component {
    render() {
        return (
            <Switch>
                <ProtectedRoute exact path='/' component={Home} />
                <Route exact path='/users/overview/:startDateIndex?' component={UsersOverview} />
                <Route exact path='/users/create' component={UsersCreateOrEdit} />
                <Route exact path='/users/login' component={UserLogin} />
                <Route exact path='/users/register' component={UsersRegister} />
                <Route exact path='/users/userprofile' component={ProfilePage} />
                <ProtectedRoute exact path='/configurepassword' component={ConfigurePassword} />
                <ProtectedRoute exact path='/production' component={ProductionTask} />
                <ProtectedRoute exact path='/production/packaging' component={PackagingTask} />
                <ProtectedRoute exact path='/production/editpackaging' component={EditPackaging} />
                <ProtectedRoute exact path='/production/keywording' component={KeywordingTask} />
                <ProtectedRoute exact path='/production/editkeywording' component={EditKeywording} />
                <ProtectedRoute exact path='/production/uploading' component={UploadingTask} />
                <ProtectedRoute exact path='/production/edituploading' component={EditUploading} />
                <ProtectedRoute exact path='/production/addoredit' component={ProductionAddOrEditTask} />
                <ProtectedRoute exact path='/masters/users' component={MasterUsers} />
                <ProtectedRoute exact path='/masters/users/register' component={MasterUsersRegister} />
                <ProtectedRoute exact path='/masters/divisions' component={MasterDivision} />
                <ProtectedRoute exact path='/masters/divisions/creatediv' component={DivisionCreateOrEdit} />
                <ProtectedRoute exact path='/masters/category' component={MasterCategory} />
                <ProtectedRoute exact path='/masters/themes' component={MasterThemes} />
                <ProtectedRoute exact path='/overview' component={Overview} />
                <ProtectedRoute exact path='/brief' component={Brief} />
                <ProtectedRoute exact path='/brief/addoreditbrief' component={AddOrEditBrief} />
                <Route path='*' exact component={PageNotFound} />

            </Switch>
        )
    }
}

export default (AllRoutes);

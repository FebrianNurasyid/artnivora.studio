import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import usersReducer from './users/reducers/usersReducer';
import authReducer from 'hvb-shared-frontend/src/store/reducers/authReducer';
import messagesReducer from './messages/reducers/messagesReducer';
import productionsReducer from './productions/reducers/productionsReducer';

export default function configureStore(history, initialState) {
    const reducers = {
        users: usersReducer,
        auth: authReducer,
        messages: messagesReducer,
        productions: productionsReducer
    };

    const middleware = [
        thunk,
        routerMiddleware(history)
    ];

    // In development, use the browser's Redux dev tools extension if installed
    const enhancers = [];
    const isDevelopment = process.env.NODE_ENV === 'development';
    if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
        enhancers.push(window.devToolsExtension());
    }

    const rootReducer = combineReducers({
        ...reducers,
        routing: routerReducer
    });

    return createStore(
        rootReducer,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
}

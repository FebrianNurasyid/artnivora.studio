import {
    uploadAttachmentProductionType,
    fetchProductionsType,
    fetchProdcutionToEditType,
    clearProdToEditType,
    clearAttachmentType,
} from '../../constants';

import { Map } from 'immutable';

const initialState = { productions: [], production: [], isLoading: false };

export default (state, action) => {
    state = state || initialState;

    if (action.type === uploadAttachmentProductionType) {
        return {
            ...state,
            attachmentProduction: action.attachmentProduction
        }
    }

    if (action.type === fetchProductionsType) {
        return {
            ...state,
            productions: action.productions,
            isLoading: false
        };
    }

    if (action.type === fetchProdcutionToEditType) {
        return {
            ...state,
            prodToEdit: action.prodToEdit
        }
    }

    if (action.type === clearProdToEditType) {
        return {
            ...state,
            prodToEdit: null,
        }
    }
    if (action.type === clearAttachmentType ) {
        return {
            ...state,
            attachmentProduction: null,
        }
    }    

    return state;
};

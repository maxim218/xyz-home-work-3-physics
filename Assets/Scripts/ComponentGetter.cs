using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentGetter {
    public static Dictionary <string, Component> GetDictionaryComponents(GameObject targetObj) {
        // all components
        Type type = typeof(Component);
        Component [] components = targetObj.GetComponents(type);
        
        // dictionary
        Dictionary <string, Component> dictionary = new Dictionary <string, Component> ();
        
        // fill dictionary
        foreach (Component component in components) {
            string key = component.GetType().ToString();
            key = key.Replace("UnityEngine.", string.Empty);
            key = key.Trim();
            dictionary[key] = component;
        }
        
        // return result
        return dictionary;
    }
}

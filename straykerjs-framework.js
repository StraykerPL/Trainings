// StraykerJS - A lightweight React-like framework

// Virtual DOM node representation
class VNode {
  constructor(type, props, children) {
    this.type = type;
    this.props = props || {};
    this.children = children || [];
    this.key = props ? props.key : undefined;
  }
}

// StraykerJS core functionality
const Strayker = {
  // Create a virtual DOM element
  createElement(type, props, ...children) {
    const flatChildren = children.flat().map(child => 
      typeof child === 'string' || typeof child === 'number' ? 
        new VNode('TEXT_ELEMENT', { nodeValue: child }, []) : child
    );
    return new VNode(type, props, flatChildren);
  },

  // Render a virtual DOM tree to a real DOM container
  render(vnode, container) {
    // Clear the container
    if (!this._root) {
      this._root = container;
      this._root._vnode = null;
    }
    
    // Perform reconciliation
    this._reconcile(container, vnode, container._vnode);
    container._vnode = vnode;
  },

  // Reconciliation algorithm
  _reconcile(parent, newVNode, oldVNode) {
    // Case 1: No old node, create new
    if (!oldVNode) {
      this._createDom(newVNode, parent);
      return;
    }
    
    // Case 2: No new node, remove old
    if (!newVNode) {
      parent.removeChild(this._findDomNode(oldVNode));
      return;
    }
    
    // Case 3: Different node types, replace old with new
    if (oldVNode.type !== newVNode.type) {
      const oldDom = this._findDomNode(oldVNode);
      const newDom = this._createDom(newVNode);
      parent.replaceChild(newDom, oldDom);
      return;
    }
    
    // Case 4: Same type, update props and children
    if (newVNode.type === 'TEXT_ELEMENT') {
      const domNode = this._findDomNode(oldVNode);
      if (newVNode.props.nodeValue !== oldVNode.props.nodeValue) {
        domNode.nodeValue = newVNode.props.nodeValue;
      }
    } else {
      const domNode = this._findDomNode(oldVNode);
      newVNode._dom = domNode;
      
      // Update props
      this._updateProps(domNode, newVNode.props, oldVNode.props);
      
      // Update children
      this._reconcileChildren(domNode, newVNode.children, oldVNode.children);
    }
  },
  
  // Create a new DOM element from a virtual node
  _createDom(vnode, parent) {
    let dom;
    
    if (vnode.type === 'TEXT_ELEMENT') {
      dom = document.createTextNode(vnode.props.nodeValue);
    } else if (typeof vnode.type === 'function') {
      // Handle function components
      const componentVNode = vnode.type(vnode.props);
      dom = this._createDom(componentVNode);
      
      // Store the component's rendered output
      vnode._component = componentVNode;
    } else {
      dom = document.createElement(vnode.type);
      this._updateProps(dom, vnode.props, {});
      
      // Create child elements
      vnode.children.forEach(child => {
        this._createDom(child, dom);
      });
    }
    
    // Store DOM node reference
    vnode._dom = dom;
    
    // Append to parent if provided
    if (parent) {
      parent.appendChild(dom);
    }
    
    return dom;
  },
  
  // Update DOM properties
  _updateProps(dom, newProps, oldProps) {
    // Remove old props
    Object.keys(oldProps).forEach(key => {
      if (key !== 'children' && key !== 'key' && !(key in newProps)) {
        if (key.startsWith('on')) {
          // Remove event listeners
          const eventType = key.slice(2).toLowerCase();
          dom.removeEventListener(eventType, oldProps[key]);
        } else {
          // Remove attribute
          dom[key] = '';
        }
      }
    });
    
    // Add/update new props
    Object.keys(newProps).forEach(key => {
      if (key !== 'children' && key !== 'key' && newProps[key] !== oldProps[key]) {
        if (key.startsWith('on')) {
          // Add event listeners
          const eventType = key.slice(2).toLowerCase();
          
          // Remove old listener if it exists
          if (oldProps[key]) {
            dom.removeEventListener(eventType, oldProps[key]);
          }
          
          // Add new listener
          dom.addEventListener(eventType, newProps[key]);
        } else if (key === 'style') {
          // Handle style object
          Object.assign(dom.style, newProps[key]);
        } else {
          // Set attribute
          dom[key] = newProps[key];
        }
      }
    });
  },
  
  // Reconcile children
  _reconcileChildren(parent, newChildren, oldChildren) {
    const maxLen = Math.max(newChildren.length, oldChildren.length);
    
    for (let i = 0; i < maxLen; i++) {
      this._reconcile(
        parent,
        i < newChildren.length ? newChildren[i] : null,
        i < oldChildren.length ? oldChildren[i] : null
      );
    }
  },
  
  // Find the DOM node for a virtual node
  _findDomNode(vnode) {
    if (!vnode) return null;
    if (vnode._dom) return vnode._dom;
    if (vnode._component) return this._findDomNode(vnode._component);
    return null;
  }
};

// Component class
class Component {
  constructor(props) {
    this.props = props || {};
    this.state = {};
  }
  
  setState(partialState) {
    // Merge the new state with existing state
    this.state = { ...this.state, ...partialState };
    
    // Trigger re-render
    if (this._currentVNode) {
      const container = this._currentVNode._dom.parentNode;
      const newVNode = this.render();
      Strayker._reconcile(container, newVNode, this._currentVNode);
      this._currentVNode = newVNode;
    }
  }
  
  render() {
    // Override in subclasses
    return null;
  }
}

// Export the StraykerJS API
Strayker.Component = Component;

// Example usage:
// const App = () => {
//   return Strayker.createElement('div', { className: 'app' },
//     Strayker.createElement('h1', null, 'Hello, StraykerJS!')
//   );
// };
// Strayker.render(Strayker.createElement(App), document.getElementById('root'));
